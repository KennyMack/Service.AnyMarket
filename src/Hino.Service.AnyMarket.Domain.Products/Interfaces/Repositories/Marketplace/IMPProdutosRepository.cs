using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace
{
    public interface IMPProdutosRepository :
        IBaseReaderRepository<MPProdutos>,
        IBaseWriterRepository<MPProdutos>
    {
        Task<IEnumerable<MPProdutos>> GetProductsAPIAsync(CancellationToken cancellation);
        Task<FSVWProdutosDetalhes> GetProductDetailsAsync(CancellationToken cancellation, int pCodEstab, string pCodProduto);
    }
}
