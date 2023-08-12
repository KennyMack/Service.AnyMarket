using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace
{
    public interface IMPCategoriasRepository :
        IBaseReaderRepository<MPCategorias>,
        IBaseWriterRepository<MPCategorias>
    {
        Task<IEnumerable<FSFamiliaNiveis>> GetFamiliasERPAsync(CancellationToken cancellation);
        bool AddCategorias(List<MPCategorias> categorias);
        bool UpdateCategorias(List<MPCategorias> categorias);
    }
}
