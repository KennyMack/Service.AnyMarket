using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities.CRM;

namespace Hino.Service.AnyMarket.Domain.Orders.Interfaces.Repositories
{
    public interface ICROrdersRepository :
        IBaseReaderRepository<CROrders>,
        IBaseWriterRepository<CROrders>
    {
    }
}
