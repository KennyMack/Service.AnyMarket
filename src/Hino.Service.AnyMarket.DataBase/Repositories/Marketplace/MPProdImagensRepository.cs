using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.DataBase.Repositories.Marketplace
{
    public class MPProdImagensRepository : BaseRepository<MPProdImagens>, IMPProdImagensRepository
    {
        public MPProdImagensRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<MPProdImagens> QuerySorted(IQueryable<MPProdImagens> source)
        {
            return source.OrderByDescending(r => r.CODCONTROLE);
        }
    }
}
