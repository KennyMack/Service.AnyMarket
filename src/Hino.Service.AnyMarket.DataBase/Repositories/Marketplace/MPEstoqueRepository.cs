using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Stock.Interfaces.Repositories;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.DataBase.Repositories.Marketplace
{
    public class MPEstoqueRepository : BaseRepository<MPEstoque>, IMPEstoqueRepository
    {
        public MPEstoqueRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<MPEstoque> QuerySorted(IQueryable<MPEstoque> source)
        {
            return source.OrderByDescending(r => r.CODCONTROLE);
        }
    }
}
