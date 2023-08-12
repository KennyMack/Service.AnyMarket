using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace
{
    public interface IMPProdPrecoRepository :
        IBaseReaderRepository<MPProdPreco>,
        IBaseWriterRepository<MPProdPreco>
    {
    }
}
