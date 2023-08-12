using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Categorias
{
    public interface IMPCategoriasDownloadService : IDisposable, IErrorBaseService
    {
        Task UpdateERPAsync(CancellationToken cancellation, List<MPCategorias> pCategorias);
    }
}
