using Hino.Service.AnyMarket.Application.Core.Interfaces;

namespace Hino.Service.AnyMarket.Application.Products.Interfaces
{
    public interface IMPTiposVariacaoAS : IDisposable, IErrorBaseAppService
    {
        Task ManageUploadAsync(CancellationToken cancellation);
    }
}
