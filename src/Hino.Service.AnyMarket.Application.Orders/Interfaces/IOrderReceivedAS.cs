using Hino.Service.AnyMarket.Application.Core.Interfaces;

namespace Hino.Service.AnyMarket.Application.Orders.Interfaces
{
    public interface IOrderReceivedAS: IDisposable, IErrorBaseAppService
    {
        public Task ReceiveNewOrdersAsync(CancellationToken cancellationToken);
    }
}
