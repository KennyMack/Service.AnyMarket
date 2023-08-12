using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Products
{
    public interface IMPProductsManageService : IDisposable, IErrorBaseService
    {
        Task<IEnumerable<MPProdutos>> GetProductsToUploadAsync(CancellationToken cancellation);
    }
}
