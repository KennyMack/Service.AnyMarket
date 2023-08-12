using Hino.Service.AnyMarket.Domain.Stock.Interfaces.Repositories;
using Hino.Service.AnyMarket.Domain.Stock.Interfaces.Services;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Logs;
using Hino.Service.AnyMarket.Utils.Paging;

namespace Hino.Service.AnyMarket.Domain.Stock.Services
{
    public class MPEstoqueManageService : IMPEstoqueManageService
    {
        public List<string> Errors { get; set; }

        readonly IMPEstoqueRepository MPEstoqueRepository;
        readonly IFSVWProdSaldoEstGrupoDisponivelRepository FSVWProdSaldoEstGrupoDisponivelRepository;

        public MPEstoqueManageService(
            IMPEstoqueRepository pRepository, 
            IFSVWProdSaldoEstGrupoDisponivelRepository fSVWProdSaldoEstGrupoDisponivelRepository)
        {
            Errors = new List<string>();
            MPEstoqueRepository = pRepository;
            FSVWProdSaldoEstGrupoDisponivelRepository = fSVWProdSaldoEstGrupoDisponivelRepository;
        }

        public async Task<IEnumerable<MPEstoque>> GetStocksToUploadAsync(CancellationToken cancellation)
        {
            try
            {
                var lst = await MPEstoqueRepository.QueryAsync(cancellation, x => x.IDAPI == null);
                return lst;
            }
            catch (Exception ex)
            {
                var msg = "Não foi possível gerar a lista de locais de estoque do erp";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return null;
            }
        }

        public async Task UpdateERPAsync(CancellationToken cancellation, List<MPEstoque> pEstoques)
        {
            foreach (var estoque in pEstoques)
            {
                try
                {
                    var OldStock = await MPEstoqueRepository.GetByKeyAsync(cancellation, r =>
                        r.CODCONTROLE == estoque.CODCONTROLE);

                    if (OldStock != null)
                    {
                        OldStock.IDAPI = estoque.IDAPI;

                        MPEstoqueRepository.Update(OldStock);

                    }

                    await MPEstoqueRepository.SaveChangesAsync(cancellation);
                }
                catch (Exception ex)
                {
                    var ms = $"Não foi possível atualizar o ERP, do loc. estoque CODCONTROLE: {estoque.CODCONTROLE} e ESTOQUE: {estoque.DESCRICAO}";
                    Errors.Add(ms);
                    Logger.LogError(ms, ex);
                }
            }
        }

        public async Task UpdateEstoqueSincAsync(CancellationToken cancellation)
        {
            try
            {
                await FSVWProdSaldoEstGrupoDisponivelRepository.UpdateEstoqueSincAsync(cancellation);
            }
            catch (Exception ex)
            {
                var msg = "Não foi possível atualizar o backup do estoque do erp";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
            }
        }

        public async Task<PagedResult<FSVWProdSaldoEstGrupoDisponivel>> GetListEstoqueDispToUploadAsync(CancellationToken cancellation, int pageNum)
        {
            try
            {
                return await FSVWProdSaldoEstGrupoDisponivelRepository.GetListEstoqueDispToUploadAsync(cancellation, pageNum);
            }
            catch (Exception ex)
            {
                var msg = "Não foi possível atualizar o backup do estoque do erp";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return new PagedResult<FSVWProdSaldoEstGrupoDisponivel>
                {
                    HasNext = false,
                    CurrentPage = pageNum,
                    PageCount = 0,
                    PageSize = 150
                };
            }
        }

        public void Dispose()
        {
            MPEstoqueRepository?.Dispose();
            FSVWProdSaldoEstGrupoDisponivelRepository?.Dispose();
        }
    }
}
