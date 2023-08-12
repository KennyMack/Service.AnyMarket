using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace
{
    public interface IMPMarcasRepository :
        IBaseReaderRepository<MPMarcas>,
        IBaseWriterRepository<MPMarcas>
    {
        Task<IEnumerable<FSMarca>> GetMarcasERPAsync(CancellationToken cancellation);
        bool UpdateMarcas(List<MPMarcas> marcas);
        bool AddMarcas(List<MPMarcas> marcas);
    }
}
