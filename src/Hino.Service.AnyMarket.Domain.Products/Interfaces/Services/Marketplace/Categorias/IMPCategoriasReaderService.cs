using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Categorias
{
    public interface IMPCategoriasReaderService : IDisposable, IErrorBaseService
    {
        Task<IEnumerable<FSFamiliaNiveis>> GetCategoriasERPAsync(CancellationToken cancellation);
        Task<IEnumerable<MPCategorias>> GetCategoriasAPIAsync(CancellationToken cancellation);
    }
}
