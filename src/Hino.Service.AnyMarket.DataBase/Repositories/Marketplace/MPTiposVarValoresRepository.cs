using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Entities.Marketplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.DataBase.Repositories.Marketplace
{
    public class MPTiposVarValoresRepository : BaseRepository<MPTiposVarValores>, IMPTiposVarValoresRepository
    {
        public MPTiposVarValoresRepository(ServiceContext context) : base(context)
        {
        }

        public override IOrderedQueryable<MPTiposVarValores> QuerySorted(IQueryable<MPTiposVarValores> source)
        {
            return source.OrderByDescending(r => r.CODCONTROLE);
        }
    }
}
