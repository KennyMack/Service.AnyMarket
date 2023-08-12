using Hino.Service.AnyMarket.Application.Core.Interfaces;

namespace Hino.Service.AnyMarket.Application.Products.Interfaces
{
    public interface IMPMarcasManageAS : IDisposable, IErrorBaseAppService
    {
        Task ManageUploadAsync(CancellationToken cancellation);
    }
}
