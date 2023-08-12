using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Products;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Products
{
    public class MPProductsManageService : IMPProductsManageService
    {
        public List<string> Errors { get; set; }

        readonly IMPProdutosRepository MPProdutosRepository;
        readonly IMPProductsReaderService MPProductsReaderService;

        public MPProductsManageService(
            IMPProdutosRepository pRepository,
            IMPProductsReaderService pReaderService)
        {
            Errors = new List<string>();
            MPProductsReaderService = pReaderService;
            MPProdutosRepository = pRepository;
        }

        public async Task<IEnumerable<MPProdutos>> GetProductsToUploadAsync(CancellationToken cancellation)
        {
            var ProductsAPI = await MPProductsReaderService.GetProductsAPIAsync(cancellation);

            return ProductsAPI;
        }

        public void Dispose()
        {
            MPProdutosRepository?.Dispose();
            MPProductsReaderService?.Dispose();
        }
    }
}
