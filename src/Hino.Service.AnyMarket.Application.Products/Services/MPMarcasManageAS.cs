using Hino.Service.AnyMarket.Application.Core.External.AnyMarket;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Application.Products.Interfaces;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Marcas;
using Hino.Service.AnyMarket.Entities.Marketplace;
using NetSwissTools.System;

namespace Hino.Service.AnyMarket.Application.Products.Services
{
    public class MPMarcasManageAS : IMPMarcasManageAS
    {
        public List<string> Errors 
        { 
            get
            {
                var list = new List<string>();
                list.AddRange(MPMarcasManageService.Errors.ToArray());
                list.AddRange(MPMarcasUploadService.Errors.ToArray());
                list.AddRange(MPMarcasDownloadService.Errors.ToArray());

                return list;
            }
        }

        readonly IMPMarcasManageService MPMarcasManageService;
        readonly IMPMarcasUploadService MPMarcasUploadService;
        readonly IMPMarcasDownloadService MPMarcasDownloadService;
        readonly Api ApiAnyMarket;

        public MPMarcasManageAS(IMPMarcasManageService pMPMarcasManageService,
            IMPMarcasUploadService mPMarcasUploadService,
            IMPMarcasDownloadService mPMarcasDownloadService,
            Api apiAnyMarket)
        {
            ApiAnyMarket = apiAnyMarket;
            MPMarcasManageService = pMPMarcasManageService;
            MPMarcasUploadService = mPMarcasUploadService;
            MPMarcasDownloadService = mPMarcasDownloadService;
        }

        public async Task ManageUploadAsync(CancellationToken cancellation)
        {
            Logs.Logger.LogInformation($"Buscando as marcas do ERP");
            var MarcasToUpload = await MPMarcasManageService.GetMarcasToUploadAsync(cancellation);

            if (MPMarcasManageService.Errors.Any())
                return;

            Logs.Logger.LogInformation($"Gerando as marcas na MPMarcas");
            await MPMarcasManageService.GenerateMarcasAsync(cancellation, MarcasToUpload.ToList());

            if (MPMarcasManageService.Errors.Any())
                return;

            Logs.Logger.LogInformation($"Buscando as marcas para serem atualizadas no AnyMarket");

            var MarcasPendentes = await MPMarcasUploadService.GetMarcasToUploadAsync(cancellation);

            if (MPMarcasUploadService.Errors.Any())
                return;

            var MarcasSalvas = new List<MPMarcas>();

            if (MarcasPendentes.Any())
            {
                Logs.Logger.LogInformation($"Comunicando com a API do AnyMarket para envio das marcas");
                MarcasSalvas = (await SendMarcasAPIAsync(cancellation, MarcasPendentes)).ToList();
            }

            MPMarcasManageService.Errors.Clear();

            if (MarcasSalvas.Any() && !Errors.Any())
            {
                Logs.Logger.LogInformation($"Atualizando a MPMarcas com o retorno da AnyMarket");
                await MPMarcasDownloadService.UpdateERPAsync(cancellation, MarcasSalvas);

                if (MPMarcasDownloadService.Errors.Any())
                    return;
            }
        }

        public async Task<IEnumerable<MPMarcas>> SendMarcasAPIAsync(CancellationToken cancellation, IEnumerable<MPMarcas> marcasToSend)
        {
            if (marcasToSend.Any())
            {
                foreach (var marca in marcasToSend)
                {
                    var brand = BrandDTO.FromEntity(marca);
                    try
                    {
                        ResponseBaseDTO<BrandDTO> Result;

                        Result = await ApiAnyMarket.Brands.GetByIdAsync<BrandDTO>(cancellation, (brand.id ?? 0).ToString());

                        if (Result.Items == null)
                        {
                            brand.id = null;
                            Result = await ApiAnyMarket.Brands.PostAsync(cancellation, brand);
                        }
                        else
                            Result = await ApiAnyMarket.Brands.PutAsync(cancellation, brand.id.ToString(), brand);

                        await Task.Delay(200);

                        ApiAnyMarket.Brands.GenerateLogResult(Result);

                        if (Result.IsSuccessful && Result.Items?.Length > 0)
                            brand.id = Result.Items[0].id;
                    }
                    catch (Exception ex)
                    {
                        Logs.Logger.LogError($"Falha na URN:{ApiAnyMarket.Brands.URN} do recurso: {ApiAnyMarket.Brands.CurrentResource}, Exception: {ex.Message}", ex);
                    }

                    marca.IDAPIPARTNER = ConvertEx.ToInt64(brand.partnerId);
                    marca.IDAPI = brand.id;
                }
            }

            return marcasToSend;
        }

        public void Dispose()
        {
            MPMarcasManageService?.Dispose();
            MPMarcasUploadService?.Dispose();
            MPMarcasDownloadService?.Dispose();
        }
    }
}
