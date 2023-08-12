using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Stock.Interfaces.Repositories;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Utils.Paging;
using Microsoft.EntityFrameworkCore;

namespace Hino.Service.AnyMarket.DataBase.Repositories.Fiscal
{
    public class FSVWProdSaldoEstGrupoDisponivelRepository : BaseRepository<FSVWProdSaldoEstGrupoDisponivel>, IFSVWProdSaldoEstGrupoDisponivelRepository
    {
        public FSVWProdSaldoEstGrupoDisponivelRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<FSVWProdSaldoEstGrupoDisponivel> QuerySorted(IQueryable<FSVWProdSaldoEstGrupoDisponivel> source)
        {
            return source.OrderByDescending(r => r.CODPRODUTO);
        }

        public async Task<PagedResult<FSVWProdSaldoEstGrupoDisponivel>> GetListEstoqueDispToUploadAsync(CancellationToken cancellation, int pageNum)
        {
            return await GetAllPagedFilteredAsync(cancellation, pageNum, 150, x => x.IDAPISKU > 0);
        }

        public async Task UpdateEstoqueSincAsync(CancellationToken cancellation)
        {
            await DbConn.Database.OpenConnectionAsync(cancellation);

            var sql = @"BEGIN PCKG_INTANYMARKET.UPDATEESTOQUESINC(); END;";

            await DbConn.ExecuteSqlCommandAsync(sql);
        }
    }
}
