using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Utils.Paging;

namespace Hino.Service.AnyMarket.Domain.Stock.Interfaces.Services
{
    public interface IMPEstoqueManageService : IDisposable, IErrorBaseService
    {
        Task<PagedResult<FSVWProdSaldoEstGrupoDisponivel>> GetListEstoqueDispToUploadAsync(CancellationToken cancellation, int pageNum);
        Task<IEnumerable<MPEstoque>> GetStocksToUploadAsync(CancellationToken cancellation);
        Task UpdateERPAsync(CancellationToken cancellation, List<MPEstoque> pEstoques);
        Task UpdateEstoqueSincAsync(CancellationToken cancellation);
    }
}
