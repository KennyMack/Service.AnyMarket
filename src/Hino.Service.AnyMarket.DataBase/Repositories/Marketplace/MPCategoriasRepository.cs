using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Microsoft.EntityFrameworkCore;

namespace Hino.Service.AnyMarket.DataBase.Repositories.Marketplace
{
    public class MPCategoriasRepository : BaseRepository<MPCategorias>, IMPCategoriasRepository
    {
        public MPCategoriasRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<MPCategorias> QuerySorted(IQueryable<MPCategorias> source)
        {
            return source.OrderByDescending(r => r.CODCONTROLE);
        }

        public async Task<IEnumerable<FSFamiliaNiveis>> GetFamiliasERPAsync(CancellationToken cancellation)
        {
            var sql = @"SELECT A.CODFAMILIA, A.DESCRICAO DESCRICAOFAMILIA,
                               B.CODGRUPO, B.DESCRICAO DESCRICAOGRUPO,
                               C.CODCLASSE, C.DESCRICAO DESCRICAOCLASSE,
                               D.CODCATEGORIA, D.DESCRICAO DESCRICAOCATEGORIA
                          FROM FSFAMILIAPRODUTO A,
                               FSGRUPOFAMILIA B,
                               FSCLASSEFAMILIA C,
                               FSCATEGORIAFAMILIA D
                         WHERE B.CODGRUPO     = A.CODGRUPO
                           AND C.CODCLASSE    = B.CODCLASSE
                           AND D.CODCATEGORIA = C.CODCATEGORIA
                           AND EXISTS (
                             SELECT 1
                               FROM MPPRODUTOS E,
                                    FSPRODUTOPARAMESTAB F
                              WHERE E.CODESTAB   = F.CODESTAB
                                AND E.CODPRODUTO = F.CODPRODUTO
                                AND F.CODFAMILIA = A.CODFAMILIA
                           )";

            await DbConn.Database.OpenConnectionAsync(cancellation);
            return await DbConn.FSFamiliaNiveis.FromSqlRaw(sql)
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public bool AddCategorias(List<MPCategorias> categorias)
        {
            DbConn.MPCategorias.AddRange(categorias);
            return true;
        }

        public bool UpdateCategorias(List<MPCategorias> categorias)
        {
            foreach (var item in categorias)
                Update(item);
            return true;
        }
    }
}
