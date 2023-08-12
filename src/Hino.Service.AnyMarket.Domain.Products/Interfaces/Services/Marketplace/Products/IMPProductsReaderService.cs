using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Products
{
    public interface IMPProductsReaderService : IDisposable, IErrorBaseService
    {
        Task<IEnumerable<MPProdutos>> GetProductsAPIAsync(CancellationToken cancellation);
    }
}
