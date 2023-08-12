using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Stock.Interfaces.Repositories
{
    public interface IMPEstoqueRepository :
        IBaseReaderRepository<MPEstoque>,
        IBaseWriterRepository<MPEstoque>
    {
    }
}
