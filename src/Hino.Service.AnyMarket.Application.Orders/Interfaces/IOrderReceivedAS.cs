using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.Interfaces;

namespace Hino.Service.AnyMarket.Application.Orders.Interfaces
{
    public interface IOrderReceivedAS: IDisposable, IErrorBaseAppService
    {
        Task FetchFeedOrdersAsync(CancellationToken cancellationToken);
        Task CreateQueueItemAsync(FeedOrderDTO feedDto, CancellationToken cancellationToken);
        Task ReceiveNewOrdersAsync(CancellationToken cancellationToken);
        Task ConfirmOrdersReceivedAsync(CancellationToken cancellationToken);
    }
}
