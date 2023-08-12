using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.DataBase.Repositories.Marketplace
{
    public class MPMarcasRepository : BaseRepository<MPMarcas>, IMPMarcasRepository
    {
        public MPMarcasRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<MPMarcas> QuerySorted(IQueryable<MPMarcas> source)
        {
            return source.OrderByDescending(r => r.CODCONTROLE);
        }

        public async Task<IEnumerable<FSMarca>> GetMarcasERPAsync(CancellationToken cancellation)
        {
            var sql = @"SELECT A.CODMARCA, A.DESCRICAO
                          FROM FSMARCA A";

            return await DbConn.FSMarca.FromSqlRaw(sql)
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public bool AddMarcas(List<MPMarcas> marcas)
        {
            DbConn.MPMarcas.AddRange(marcas);
            return true;
        }

        public bool UpdateMarcas(List<MPMarcas> marcas)
        {
            foreach (var item in marcas)
                Update(item);
            return true;
        }
    }
}
