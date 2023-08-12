using Hino.Service.AnyMarket.Application.Core.External.AnyMarket;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Application.Products.Interfaces;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Products;
using Hino.Service.AnyMarket.Entities.Marketplace;
using NetSwissTools.Utils;
using Serilog;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hino.Service.AnyMarket.Application.Products.Services
{
    public class MPProdutosManageAS : IMPProdutosManageAS
    {
        public List<string> Errors
        {
            get
            {
                var list = new List<string>();
                list.AddRange(MPProductsManageService.Errors.ToArray());
                list.AddRange(MPProductsUploadService.Errors.ToArray());
                list.AddRange(MPProductsDownloadService.Errors.ToArray());

                return list;
            }
        }

        readonly IMPProductsManageService MPProductsManageService;
        readonly IMPProductsUploadService MPProductsUploadService;
        readonly IMPProductsDownloadService MPProductsDownloadService;
        readonly Api ApiAnyMarket;

        public MPProdutosManageAS(IMPProductsManageService pMPProductsManageService,
            IMPProductsUploadService mPProductsUploadService,
            IMPProductsDownloadService mPProductsDownloadService,
            Api apiAnyMarket)
        {
            ApiAnyMarket = apiAnyMarket;
            MPProductsManageService = pMPProductsManageService;
            MPProductsUploadService = mPProductsUploadService;
            MPProductsDownloadService = mPProductsDownloadService;
        }

        public async Task ManageUploadAsync(CancellationToken cancellation)
        {
            Logs.Logger.LogInformation($"Buscando os produtos do ERP");

            Logs.Logger.LogInformation($"Buscando os produtos para serem atualizados no AnyMarket");

            var Pendentes = await MPProductsManageService.GetProductsToUploadAsync(cancellation);

            if (MPProductsUploadService.Errors.Any())
                return;

            var Salvos = new List<MPProdutos>();

            if (Pendentes.Any())
            {
                Logs.Logger.LogInformation($"Comunicando com a API do AnyMarket para envio dos produtos");
                Salvos = (await SendToAPIAsync(cancellation, Pendentes)).ToList();
            }

            MPProductsDownloadService.Errors.Clear();
            
            if (Salvos.Any() && !Errors.Any())
            {
                Logs.Logger.LogInformation($"Atualizando a MPProdutos com o retorno da AnyMarket");
                await MPProductsDownloadService.UpdateERPAsync(cancellation, Salvos);

                if (MPProductsDownloadService.Errors.Any())
                    return;
            }
        }

        async Task<IEnumerable<MPProdutos>> SendToAPIAsync(CancellationToken cancellation, IEnumerable<MPProdutos> listToSend)
        {
            if (listToSend.Any())
            {
                var MainProducts = listToSend.Where(x => x.CODPRODUTO == x.CODGRUPOVARIACAO);

                foreach (var produto in MainProducts)
                {
                    var log = produto.IsValidToUpload();

                    if (!log.IsEmpty())
                    {
                        produto.STATUSSINC = 2;
                        produto.PROBLEMA = log.SubStr(0, 2000);
                        foreach (var filho in listToSend.Where(x => x.CODGRUPOVARIACAO == produto.CODGRUPOVARIACAO))
                        {
                            filho.STATUSSINC = 2;
                            filho.PROBLEMA = log.SubStr(0, 2000);
                        }

                        Logs.Logger.LogError(log);
                        continue;
                    }

                    var prodDTO = ProductDTO.FromEntity(produto, listToSend.Where(x => x.CODGRUPOVARIACAO == produto.CODGRUPOVARIACAO));

                    var resultProduct = await SendProductToAPIAsync(cancellation, prodDTO, produto.CODPRODUTO);

                    produto.IDAPI = resultProduct.id;
                    produto.STATUSSINC = (short)((produto.IDAPI > 0) ? 1 : 2);

                    foreach (var filho in listToSend.Where(x => x.CODGRUPOVARIACAO == produto.CODGRUPOVARIACAO))
                    {
                        filho.STATUSSINC = produto.STATUSSINC;
                        filho.IDAPI = resultProduct.id;

                        var sku = resultProduct.skus.FirstOrDefault(x => x.partnerId == filho.CODPRODUTO);
                        filho.IDAPISKU = sku?.id;
                    }

                    foreach (var imagem in produto.ProdImagens)
                    {
                        var image = resultProduct.images.FirstOrDefault(x => x.index == imagem.INDICE);
                        imagem.IDAPI = image?.id;
                    }


                    // listToSend.First(x => x.CODCONTROLE == produto.CODCONTROLE)
                    //     .IDAPI = resultProduct.id;
                    // listToSend.First(x => x.CODCONTROLE == produto.CODCONTROLE)
                    //     .STATUSSINC = (short)((produto.IDAPI > 0) ? 1 : 2);

                    // var str = prodDTO.ToString();
                }

                /*
                var WithoutVariations = listToSend.Where(x => x.CODVARIACAOVLR == null).ToList();
                var WithVariations = listToSend.Where(x => x.CODVARIACAOVLR != null).ToList();

                foreach (var produto in WithoutVariations)
                {
                    var log = produto.IsValidToUpload();

                    if (!log.IsEmpty())
                    {
                        produto.STATUSSINC = 2;
                        Logs.Logger.LogError(log);
                        continue;
                    }

                    var resultProduct = await SendProductToAPIAsync(cancellation, ProductDTO.FromEntity(produto), produto.CODPRODUTO);

                    produto.IDAPI = resultProduct.id;
                    produto.STATUSSINC = (short)((produto.IDAPI > 0) ? 1 : 2);

                    listToSend.First(x => x.CODCONTROLE == produto.CODCONTROLE)
                        .IDAPI = resultProduct.id;
                    listToSend.First(x => x.CODCONTROLE == produto.CODCONTROLE)
                        .STATUSSINC = (short)((produto.IDAPI > 0) ? 1 : 2);
                }
                */
            }

            return listToSend;
        }

        async Task<ProductDTO> SendProductToAPIAsync(CancellationToken cancellation, ProductDTO product, string pCodProduto)
        {
            try
            {
                ResponseBaseDTO<ProductDTO> Result;

                Result = await ApiAnyMarket.Products.GetByIdAsync<ProductDTO>(cancellation, (product.id ?? 0).ToString());

                if (Result.Items == null)
                {
                    product.id = null;
                    Result = await ApiAnyMarket.Products.PostAsync(cancellation, product);
                }
                else
                {
                    Result = await ApiAnyMarket.Products.PutAsync(cancellation, product.id.ToString(), product);

                    if (Result.IsSuccessful && Result.Items?.Length > 0)
                    {
                        foreach (var sku in product.skus)
                        {
                            var ResultApiSku = await ApiAnyMarket.Skus.GetByIdAsync(cancellation, product.id.ToString(), sku.id.ToString());

                            if (ResultApiSku.Items == null)
                            {
                                sku.id = null;
                                ResultApiSku = await ApiAnyMarket.Skus.PostAsync(cancellation, product.id.ToString(), sku);
                            }
                            else
                                ResultApiSku = await ApiAnyMarket.Skus.PutAsync(cancellation, product.id.ToString(), sku.id.ToString(), sku);

                            if (ResultApiSku.IsSuccessful && ResultApiSku.Items?.Length > 0)
                                sku.id = ResultApiSku.Items[0].id;

                            await Task.Delay(120);
                        }

                        var ResultApiSkusList = await ApiAnyMarket.Skus.GetAsync(cancellation, product.id.ToString());

                        if (ResultApiSkusList.IsSuccessful && ResultApiSkusList.Items?.Length > 0)
                        {
                            foreach (var sku in ResultApiSkusList.Items)
                            {
                                var skuExists = product.skus.Any(x => x.partnerId == sku.partnerId);

                                if (!skuExists)
                                {
                                    await ApiAnyMarket.Skus.DeleteAsync(cancellation, product.id.ToString(), sku.id.ToString());
                                    await Task.Delay(120);
                                }
                            }
                        }

                        ResultApiSkusList = await ApiAnyMarket.Skus.GetAsync(cancellation, product.id.ToString());

                        if (ResultApiSkusList.IsSuccessful && ResultApiSkusList.Items?.Length > 0)
                        {
                            var skus = ResultApiSkusList.Items;
                            foreach (var sku in Result.Items[0].skus)
                            {
                                var skuapi = skus.FirstOrDefault(x => x.partnerId == sku.partnerId);
                                sku.id = skuapi?.id;
                            }
                        }
                    }
                }
                await Task.Delay(120);

                ApiAnyMarket.Products.GenerateLogResult(Result);

                if (Result.IsSuccessful && Result.Items?.Length > 0)
                {
                    product.id = Result.Items[0].id;
                    var skus = Result.Items[0].skus;

                    foreach (var sku in product.skus)
                    {
                        var skuapi = skus.FirstOrDefault(x => x.partnerId == sku.partnerId);
                        sku.id = skuapi?.id;
                    }

                    var ResultProductImg = await ApiAnyMarket.Images.GetAsync(cancellation, product.id.ToString());

                    if (ResultProductImg.IsSuccessful && ResultProductImg.Items?.Length > 0)
                    {
                        foreach (var item in ResultProductImg.Items)
                        {
                            await ApiAnyMarket.Images.DeleteAsync(cancellation, product.id.ToString(), item.id.ToString());
                            await Task.Delay(120);
                        }
                    }

                    if (product.images != null &&
                        (product.images?.Any() ?? false))
                    {
                        var orderedImages = product.images.OrderByDescending(r => r.main);

                        foreach (var item in orderedImages)
                        {
                            item.id = null;
                            var ResultImgCreate = await ApiAnyMarket.Images.PostAsync(cancellation, product.id.ToString(), ImageDTO.FromEntity(item));

                            if (ResultImgCreate.IsSuccessful && ResultImgCreate.Items?.Length > 0)
                                item.id = ResultImgCreate.Items[0].id;

                            await Task.Delay(120);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                product.id = -2;
                Logs.Logger.LogError($"Falha na URN:{ApiAnyMarket.Products.URN} do recurso: {ApiAnyMarket.Products.CurrentResource} para o produto: {pCodProduto}, Exception: {ex.Message}", ex);
            }

            return product;
        }

        public void Dispose()
        {
            MPProductsManageService?.Dispose();
            MPProductsUploadService?.Dispose();
            MPProductsDownloadService?.Dispose();
        }
    }
}
