using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace
{
    public interface IMPTiposVariacaoRepository :
        IBaseReaderRepository<MPTiposVariacao>,
        IBaseWriterRepository<MPTiposVariacao>
    {
    }
}
