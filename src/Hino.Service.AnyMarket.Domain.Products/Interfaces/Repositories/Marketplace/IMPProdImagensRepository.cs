using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace
{
    public interface IMPProdImagensRepository :
        IBaseReaderRepository<MPProdImagens>,
        IBaseWriterRepository<MPProdImagens>
    {
    }
}
