using Hino.Service.AnyMarket.Application.Core.External.AnyMarket;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Application.Stock.Interfaces;
using Hino.Service.AnyMarket.Domain.Stock.Interfaces.Services;
using Hino.Service.AnyMarket.Domain.Stock.Services;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;
using NetSwissTools.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Stock.Services
{
    public class MPEstoqueManageAS : IMPEstoqueManageAS
    {
        public List<string> Errors
        {
            get
            {
                var list = new List<string>();
                list.AddRange(MPEstoqueManageService.Errors.ToArray());

                return list;
            }
        }

        readonly IMPEstoqueManageService MPEstoqueManageService;
        readonly Api ApiAnyMarket;

        public MPEstoqueManageAS(IMPEstoqueManageService pMPEstoqueManageService,
            Api apiAnyMarket)
        {
            ApiAnyMarket = apiAnyMarket;
            MPEstoqueManageService = pMPEstoqueManageService;
        }

        public async Task ManageUploadStockLocalAsync(CancellationToken cancellation)
        {
            Logs.Logger.LogInformation($"Buscando os locais de estoque do ERP");

            Logs.Logger.LogInformation($"Buscando os locais de estoque para serem atualizados no AnyMarket");
            var EstoquesToUpload = await MPEstoqueManageService.GetStocksToUploadAsync(cancellation);

            if (MPEstoqueManageService.Errors.Any())
                return;

            var Salvos = new List<MPEstoque>();

            if (EstoquesToUpload.Any())
            {
                Logs.Logger.LogInformation($"Comunicando com a API do AnyMarket para envio dos locais de estoque");
                Salvos = (await SendStockLocalToAPIAsync(cancellation, EstoquesToUpload)).ToList();
            }

            MPEstoqueManageService.Errors.Clear();

            if (Salvos.Any() && !Errors.Any())
            {
                Logs.Logger.LogInformation($"Atualizando a MPEstoque com o retorno da AnyMarket");
                await MPEstoqueManageService.UpdateERPAsync(cancellation, Salvos);

                if (MPEstoqueManageService.Errors.Any())
                    return;
            }
        }

        public async Task ManageUploadStockBalanceAsync(CancellationToken cancellation)
        {
            Logs.Logger.LogInformation($"Buscando os saldos de estoque do ERP");

            List<StockBalanceDTO> StockBalanceOld = new();
            List<FSVWProdSaldoEstGrupoDisponivel> StockBalanceToUpload = new();
            var Continue = true;
            var pageNum = 1;
            var retries = 0;
            do
            {
                MPEstoqueManageService.Errors.Clear();
                Logs.Logger.LogInformation($"Buscando os saldos de estoque do ERP para serem atualizados no AnyMarket. Page: {pageNum}");
                var Stocks = await MPEstoqueManageService.GetListEstoqueDispToUploadAsync(cancellation, pageNum);

                if (MPEstoqueManageService.Errors.Any())
                {
                    Logs.Logger.LogInformation($"Não foi possível buscar os saldos de estoque do ERP. tentativa: {1} de 10", new Exception(MPEstoqueManageService.Errors[0]));
                    retries++;
                    if (retries < 11)
                        continue;
                }

                StockBalanceToUpload.AddRange(Stocks.Results);

                retries = 0;
                pageNum++;

                if (!Stocks.HasNext)
                    Continue = false;
            } while (Continue);


            if (StockBalanceToUpload.Any())
            {
                Logs.Logger.LogInformation($"Comunicando com a API do AnyMarket para baixar estoques anteriores.");
                StockBalanceOld = await GetListStockBalanceFromAPIAsync(cancellation, StockBalanceToUpload);

                Logs.Logger.LogInformation($"Comunicando com a API do AnyMarket para envio dos saldos de estoque");
                await SendStockBalanceToAPIAsync(cancellation, StockBalanceOld, StockBalanceToUpload);
            }

            if (!Errors.Any())
            {
                Logs.Logger.LogInformation($"Atualizando a FSSINCSALDOESTOQUE com o retorno da AnyMarket");
                await MPEstoqueManageService.UpdateEstoqueSincAsync(cancellation);

                if (MPEstoqueManageService.Errors.Any())
                    return;
            }
        }

        async Task<IEnumerable<MPEstoque>> SendStockLocalToAPIAsync(CancellationToken cancellation, IEnumerable<MPEstoque> listToSend)
        {
            if (listToSend.Any())
            {
                foreach (var stock in listToSend)
                {
                    var stockDTO = StockLocalDTO.FromEntity(stock);
                    try
                    {
                        ResponseBaseDTO<StockLocalDTO> Result;

                        Result = await ApiAnyMarket.StocksLocals.GetByIdAsync<StockLocalDTO>(cancellation, (stockDTO.id ?? 0).ToString());

                        if (Result.Items == null)
                        {
                            stockDTO.id = null;
                            Result = await ApiAnyMarket.StocksLocals.PostAsync(cancellation, stockDTO);
                        }
                        else
                            Result = await ApiAnyMarket.StocksLocals.PutAsync(cancellation, stockDTO.id.ToString(), stockDTO);

                        await Task.Delay(200);

                        ApiAnyMarket.StocksLocals.GenerateLogResult(Result);

                        if (Result.IsSuccessful && Result.Items?.Length > 0)
                            stockDTO.id = Result.Items[0].id;
                    }
                    catch (Exception ex)
                    {
                        Logs.Logger.LogError($"Falha na URN:{ApiAnyMarket.StocksLocals.URN} do recurso: {ApiAnyMarket.StocksLocals.CurrentResource}, Exception: {ex.Message}", ex);
                    }

                    stock.IDAPI = stockDTO.id;
                }
            }

            return listToSend;
        }

        async Task<List<StockBalanceDTO>> GetListStockBalanceFromAPIAsync(CancellationToken cancellation, IEnumerable<FSVWProdSaldoEstGrupoDisponivel> listToSend)
        {
            var list = new List<StockBalanceDTO>();
            try
            {
                if (listToSend.Any())
                {
                    var pageNum = 0;
                    var pageSize = 100;
                    var Continue = true;
                    do
                    {
                        var Result = await ApiAnyMarket.Stocks.GetListAsync<StockBalanceDTO>(cancellation, pageSize, pageNum * pageSize);

                        if (!Result.IsSuccessful)
                            Continue = false;

                        if (Result.IsSuccessful)
                        {
                            list.AddRange(Result.content);

                            pageNum = (Result.page.totalPages - 1 == pageNum) ? -1 : pageNum + 1;
                        }

                        await Task.Delay(120);

                        if (pageNum == -1)
                            Continue = false;
                    } while (Continue);
                }
            }
            catch (Exception ex)
            {
                Logs.Logger.LogError($"Falha ao buscar o estoque carregado na AnyMarket", ex);
            }
            return list;
        }

        async Task<IEnumerable<FSVWProdSaldoEstGrupoDisponivel>> SendStockBalanceToAPIAsync(CancellationToken cancellation, List<StockBalanceDTO> stocksOld, IEnumerable<FSVWProdSaldoEstGrupoDisponivel> listToSend)
        {
            if (listToSend.Any())
            {
                var StocksToUpload = StockBalanceDTO.FromEntity(listToSend.ToArray());

                var StocksToPut = StocksToUpload.Where(x => stocksOld.Select(c => $"{c.id}-{c.stockLocalId}" ).Contains($"{x.id}-{x.stockLocalId}")).ToArray();
                var StocksToPost = StocksToUpload.Where(x => !stocksOld.Select(c => $"{c.id}-{c.stockLocalId}").Contains($"{x.id}-{x.stockLocalId}")).ToArray();

                var PageSize = 150;
                var PostPageCount = (int)Math.Ceiling(Convert.ToDecimal(StocksToPost.Count()) / Convert.ToDecimal(PageSize));
                var PutPageCount = (int)Math.Ceiling(Convert.ToDecimal(StocksToPut.Count()) / Convert.ToDecimal(PageSize));

                for (int i = 0, length = PostPageCount; i < length; i++)
                {
                    var StocksPage = StocksToPost.Skip(i * PageSize).Take(PageSize).ToArray();

                    var Result = await ApiAnyMarket.Stocks.PostPageAsync(cancellation, StocksPage);
                    await Task.Delay(120);
                    ApiAnyMarket.Stocks.GenerateLogResult(Result);
                }
                for (int i = 0, length = PutPageCount; i < length; i++)
                {
                    var StocksPage = StocksToPut.Skip(i * PageSize).Take(PageSize).ToArray();

                    var Result = await ApiAnyMarket.Stocks.PutPageAsync(cancellation, StocksPage);
                    await Task.Delay(120);
                    ApiAnyMarket.Stocks.GenerateLogResult(Result);
                }
            }

            return listToSend;
        }

        public void Dispose()
        {
            MPEstoqueManageService?.Dispose();
        }
    }
}
