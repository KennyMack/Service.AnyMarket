using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.General;

namespace Hino.Service.AnyMarket.Domain.Orders.Interfaces.Repositories
{
    public interface IGEQueueItemRepository :
        IBaseReaderRepository<GEQueueItem>,
        IBaseWriterRepository<GEQueueItem>
    {
    }
}
