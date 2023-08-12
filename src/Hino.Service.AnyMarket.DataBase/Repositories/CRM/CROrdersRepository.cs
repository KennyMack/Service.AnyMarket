using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Repositories;
using Hino.Service.AnyMarket.Entities.CRM;

namespace Hino.Service.AnyMarket.DataBase.Repositories.CRM
{
    public class CROrdersRepository : BaseRepository<CROrders>, ICROrdersRepository
    {
        public CROrdersRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<CROrders> QuerySorted(IQueryable<CROrders> source)
        {
            return source.OrderByDescending(r => r.ID);
        }
    }
}
