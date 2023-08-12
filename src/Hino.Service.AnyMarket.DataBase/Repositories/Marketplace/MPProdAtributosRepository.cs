using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.DataBase.Repositories.Marketplace
{
    public class MPProdAtributosRepository : BaseRepository<MPProdAtributos>, IMPProdAtributosRepository
    {
        public MPProdAtributosRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<MPProdAtributos> QuerySorted(IQueryable<MPProdAtributos> source)
        {
            return source.OrderByDescending(r => r.CODCONTROLE);
        }
    }
}
