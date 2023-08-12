using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Categorias;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Products;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Products
{
    public class MPProductsDownloadService : IMPProductsDownloadService
    {
        public List<string> Errors { get; set; }

        readonly IMPProdutosRepository MPProdutosRepository;
        readonly IMPProdImagensRepository MPProdImagensRepository;

        public MPProductsDownloadService(IMPProdutosRepository pRepository, IMPProdImagensRepository mPProdImagensRepository)
        {
            Errors = new List<string>();
            MPProdutosRepository = pRepository;
            MPProdImagensRepository = mPProdImagensRepository;
        }

        public async Task UpdateERPAsync(CancellationToken cancellation, List<MPProdutos> pProdutos)
        {
            foreach (var item in pProdutos)
            {
                try
                {
                    var Old = await MPProdutosRepository.GetByKeyAsync(cancellation, r => r.CODCONTROLE == item.CODCONTROLE);

                    if (Old != null)
                    {
                        Old.STATUSSINC = item.STATUSSINC;
                        Old.PROBLEMA = item.PROBLEMA;
                        Old.DATASINC = DateTime.Now;
                        Old.IDAPI = item.IDAPI;
                        Old.IDAPISKU = item.IDAPISKU;

                        MPProdutosRepository.Update(Old);
                    }

                    await MPProdutosRepository.SaveChangesAsync(cancellation);
                }
                catch (Exception ex)
                {
                    var ms = $"Não foi possível atualizar o ERP, do produto CODCONTROLE: {item.CODCONTROLE}";
                    Errors.Add(ms);
                    Logger.LogError(ms, ex);
                }

                try
                {
                    foreach (var image in item.ProdImagens)
                    {
                        var OldImg = await MPProdImagensRepository.GetByKeyAsync(cancellation, r => r.CODCONTROLE == image.CODCONTROLE);

                        if (OldImg != null)
                        {
                            OldImg.IDAPI = image.IDAPI;

                            MPProdImagensRepository.Update(OldImg);
                        }

                        await MPProdImagensRepository.SaveChangesAsync(cancellation);
                    }
                }
                catch (Exception ex)
                {
                    var ms = $"Não foi possível atualizar as imagens no ERP, do produto CODCONTROLE: {item.CODCONTROLE}";
                    Errors.Add(ms);
                    Logger.LogError(ms, ex);
                }
            }
        }

        public void Dispose()
        {
            MPProdutosRepository?.Dispose();
        }
    }
}
