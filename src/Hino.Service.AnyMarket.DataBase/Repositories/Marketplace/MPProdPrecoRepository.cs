using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.DataBase.Repositories.Marketplace
{
    public class MPProdPrecoRepository : BaseRepository<MPProdPreco>, IMPProdPrecoRepository
    {
        public MPProdPrecoRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<MPProdPreco> QuerySorted(IQueryable<MPProdPreco> source)
        {
            return source.OrderByDescending(r => r.CODCONTROLE);
        }
    }
}
