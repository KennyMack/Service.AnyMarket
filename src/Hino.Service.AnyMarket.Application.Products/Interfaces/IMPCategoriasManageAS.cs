using Hino.Service.AnyMarket.Application.Core.Interfaces;

namespace Hino.Service.AnyMarket.Application.Products.Interfaces
{
    public interface IMPCategoriasManageAS : IDisposable, IErrorBaseAppService
    {
        Task ManageUploadAsync(CancellationToken cancellation);
    }
}
