using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Repositories;
using Hino.Service.AnyMarket.Entities.General;

namespace Hino.Service.AnyMarket.DataBase.Repositories.General
{
    public class GEQueueItemRepository : BaseRepository<GEQueueItem>, IGEQueueItemRepository
    {
        public GEQueueItemRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<GEQueueItem> QuerySorted(IQueryable<GEQueueItem> source)
        {
            return source.OrderByDescending(r => r.ID);
        }
    }
}
