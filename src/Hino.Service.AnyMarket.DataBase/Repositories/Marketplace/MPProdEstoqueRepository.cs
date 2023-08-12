using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.DataBase.Repositories.Marketplace
{
    public class MPProdEstoqueRepository : BaseRepository<MPProdEstoque>, IMPProdEstoqueRepository
    {
        public MPProdEstoqueRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<MPProdEstoque> QuerySorted(IQueryable<MPProdEstoque> source)
        {
            return source.OrderByDescending(r => r.CODCONTROLE);
        }
    }
}
