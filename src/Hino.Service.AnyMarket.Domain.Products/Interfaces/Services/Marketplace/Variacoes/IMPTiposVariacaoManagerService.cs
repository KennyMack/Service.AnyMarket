using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Variacoes
{
    public interface IMPTiposVariacaoManagerService : IDisposable, IErrorBaseService
    {
        Task<IEnumerable<MPTiposVariacao>> GetVariacoesToUploadAsync(CancellationToken cancellation);
        Task UpdateERPAsync(CancellationToken cancellation, List<MPTiposVariacao> pTiposVariacao);
    }
}
