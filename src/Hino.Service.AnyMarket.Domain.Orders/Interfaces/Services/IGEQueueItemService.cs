using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.General;

namespace Hino.Service.AnyMarket.Domain.Orders.Interfaces.Services
{
    public interface IGEQueueItemService : IDisposable, IErrorBaseService
    {
        Task<GEQueueItem> CreateQueueItemAsync(long orderId, string token, CancellationToken cancellation);
        Task<IEnumerable<GEQueueItem>> GetAllProcessedAsync(CancellationToken cancellation);
        Task<IEnumerable<GEQueueItem>> GetAllPendingProcessedAsync(CancellationToken cancellation);
    }
}
