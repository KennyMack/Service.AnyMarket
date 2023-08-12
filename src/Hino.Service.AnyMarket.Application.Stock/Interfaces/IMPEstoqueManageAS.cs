using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hino.Service.AnyMarket.Application.Core.Interfaces;

namespace Hino.Service.AnyMarket.Application.Stock.Interfaces
{
    public interface IMPEstoqueManageAS : IDisposable, IErrorBaseAppService
    {
        Task ManageUploadStockLocalAsync(CancellationToken cancellation);
        Task ManageUploadStockBalanceAsync(CancellationToken cancellation);
    }
}
