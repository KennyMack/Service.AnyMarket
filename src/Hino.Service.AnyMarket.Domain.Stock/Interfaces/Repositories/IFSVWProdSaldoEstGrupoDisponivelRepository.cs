using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Utils.Paging;

namespace Hino.Service.AnyMarket.Domain.Stock.Interfaces.Repositories
{
    public interface IFSVWProdSaldoEstGrupoDisponivelRepository :
        IBaseReaderRepository<FSVWProdSaldoEstGrupoDisponivel>
    {
        Task<PagedResult<FSVWProdSaldoEstGrupoDisponivel>> GetListEstoqueDispToUploadAsync(CancellationToken cancellation, int pageNum);
        Task UpdateEstoqueSincAsync(CancellationToken cancellation);
    }
}
