using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Repositories;
using Hino.Service.AnyMarket.Entities.CRM;

namespace Hino.Service.AnyMarket.DataBase.Repositories.CRM
{
    public class CROrderItemsRepository : BaseRepository<CROrderItems>, ICROrderItemsRepository
    {
        public CROrderItemsRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<CROrderItems> QuerySorted(IQueryable<CROrderItems> source)
        {
            return source.OrderByDescending(r => r.ID);
        }
    }
}
