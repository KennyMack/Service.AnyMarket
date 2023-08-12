using Hino.Service.AnyMarket.Application.Core.External.AnyMarket;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Application.Products.Interfaces;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Variacoes;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Application.Products.Services
{
    public class MPTiposVariacaoAS: IMPTiposVariacaoAS
    {
        public List<string> Errors
        {
            get
            {
                var list = new List<string>();
                list.AddRange(TiposVariacaoManagerService.Errors.ToArray());

                return list;
            }
        }

        readonly IMPTiposVariacaoManagerService TiposVariacaoManagerService;
        readonly Api ApiAnyMarket;

        public MPTiposVariacaoAS(IMPTiposVariacaoManagerService pTiposVariacaoManagerService,
            Api apiAnyMarket)
        {
            ApiAnyMarket = apiAnyMarket;
            TiposVariacaoManagerService = pTiposVariacaoManagerService;
        }

        public async Task ManageUploadAsync(CancellationToken cancellation)
        {
            Logs.Logger.LogInformation($"Buscando as variaçoes para serem atualizados no AnyMarket");
            var TiposPendentes = await TiposVariacaoManagerService.GetVariacoesToUploadAsync(cancellation);

            if (TiposVariacaoManagerService.Errors.Any())
                return;

            var TiposSalvos = new List<MPTiposVariacao>();

            if (TiposPendentes.Any())
            {
                Logs.Logger.LogInformation($"Comunicando com a API do AnyMarket para envio das marcas");
                TiposSalvos = (await SendMarcasAPIAsync(cancellation, TiposPendentes)).ToList();
            }

            TiposVariacaoManagerService.Errors.Clear();

            if (TiposSalvos.Any() && !Errors.Any())
            {
                Logs.Logger.LogInformation($"Atualizando a MPMarcas com o retorno da AnyMarket");
                await TiposVariacaoManagerService.UpdateERPAsync(cancellation, TiposSalvos);

                if (TiposVariacaoManagerService.Errors.Any())
                    return;
            }
        }

        public async Task<IEnumerable<MPTiposVariacao>> SendMarcasAPIAsync(CancellationToken cancellation, IEnumerable<MPTiposVariacao> tiposToSend)
        {
            if (tiposToSend.Any())
            {
                foreach (var tipo in tiposToSend)
                {
                    var variationType = VariationTypeDTO.FromEntity(tipo);
                    try
                    {
                        ResponseBaseDTO<RetVariationTypeDTO> Result;

                        Result = await ApiAnyMarket.VariationsType.GetTypeByIdAsync(cancellation, (variationType.id ?? 0).ToString());

                        if (Result.Items == null)
                        {
                            variationType.id = null;
                            Result = await ApiAnyMarket.VariationsType.PostCreateAsync(cancellation, variationType);
                        }
                        else
                            Result = await ApiAnyMarket.VariationsType.PutChangeAsync(cancellation, variationType.id.ToString(), variationType);

                        await Task.Delay(200);

                        ApiAnyMarket.VariationsType.GenerateLogResult(Result);

                        if (Result.IsSuccessful && Result.Items?.Length > 0)
                        {
                            variationType.id = Result.Items[0].id;

                            foreach (var vartp in variationType.values)
                            {
                                var itTpValue = Result.Items[0].values.FirstOrDefault(x => x.partnerId == vartp.partnerId);
                                if (itTpValue != null)
                                    vartp.id = itTpValue.id;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Logs.Logger.LogError($"Falha na URN:{ApiAnyMarket.VariationsType.URN} do recurso: {ApiAnyMarket.VariationsType.CurrentResource}, Exception: {ex.Message}", ex);
                    }
                    tipo.IDAPI = variationType.id;


                    if (tipo.IDAPI > 0)
                    {
                        foreach (var vartp in tipo.Valores)
                        {
                            var itTpValue = variationType.values.FirstOrDefault(x => x.partnerId == vartp.CODCONTROLE.ToString());
                            if (itTpValue != null)
                                vartp.IDAPI = itTpValue.id;
                        }

                        foreach (var value in tipo.Valores)
                        {
                            var variationTypeValue = VariationValueDTO.FromEntity(value);

                            variationTypeValue.type = null;

                            try
                            {
                                ResponseBaseDTO<VariationValueDTO> Result;

                                Result = await ApiAnyMarket.VariationsValue.GetByIdAsync(cancellation, tipo.IDAPI.ToString(), (variationTypeValue.id ?? 0).ToString());

                                if (Result.Items == null)
                                {
                                    variationTypeValue.id = null;
                                    Result = await ApiAnyMarket.VariationsValue.PostAsync(cancellation, tipo.IDAPI.ToString(), variationTypeValue);
                                }
                                else
                                    Result = await ApiAnyMarket.VariationsValue.PutAsync(cancellation, tipo.IDAPI.ToString(), (variationTypeValue.id ?? 0).ToString(), variationTypeValue);

                                await Task.Delay(200);

                                ApiAnyMarket.VariationsValue.GenerateLogResult(Result);

                                if (Result.IsSuccessful && Result.Items?.Length > 0)
                                    variationTypeValue.id = Result.Items[0].id;
                            }
                            catch (Exception ex)
                            {
                                Logs.Logger.LogError($"Falha na URN:{ApiAnyMarket.VariationsValue.URN} do recurso: {ApiAnyMarket.VariationsValue.CurrentResource}, Exception: {ex.Message}", ex);
                            }

                            value.IDAPI = variationTypeValue.id;
                        }
                    }
                }
            }

            return tiposToSend;
        }

        public void Dispose()
        {
            TiposVariacaoManagerService?.Dispose();
        }
    }
}
