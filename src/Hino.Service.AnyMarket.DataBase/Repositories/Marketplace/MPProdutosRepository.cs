using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Hino.Service.AnyMarket.DataBase.Repositories.Marketplace
{
    public class MPProdutosRepository : BaseRepository<MPProdutos>, IMPProdutosRepository
    {
        public MPProdutosRepository(ServiceContext DbContext) : base(DbContext)
        {
        }

        public override IOrderedQueryable<MPProdutos> QuerySorted(IQueryable<MPProdutos> source)
        {
            return source.OrderByDescending(r => r.CODCONTROLE);
        }

        public async Task<IEnumerable<MPProdutos>> GetProductsAPIAsync(CancellationToken cancellation)
        {
            return await DbEntity
                .AsNoTracking()
                .Where(exp => exp.STATUSSINC == 0 && exp.LIBERADOSINC == 1 /*&&
                           (exp.CODGRUPOVARIACAO == "600.00944" ||
                           exp.CODGRUPOVARIACAO == "700.31057")*/)
                .Include(x => x.ProdVariacaoVlr)
                .Include(x => x.ProdVariacaoVlr.Tipo)
                .Include(x => x.ProdAtributos)
                .Include(x => x.ProdImagens)
                .ThenInclude(a => a.ProdVariacaoVlr)
                .Include(x => x.ProdImagens)
                .ThenInclude(a => a.ProdVariacaoVlr.Tipo)
                .Include(x => x.ProdPreco)
                .ToListAsync(cancellation);
        }

        public async Task<FSVWProdutosDetalhes> GetProductDetailsAsync(CancellationToken cancellation, int pCodEstab, string pCodProduto)
        {
            var sql = @"SELECT A.CODPRODUTO, A.CODESTAB, A.CODMARCA, A.DESCRICAO,
                               A.LARGURA, A.COMPRIMENTO, A.ALTURA, A.PESOBRUTO,
                               A.FAMILIA, A.GRUPO, A.CLASSE, A.CATEGORIA,
                               A.CODORIGMERC, A.SALDOESTOQUE
                          FROM FSVWPRODUTOSDETALHES A
                         WHERE A.CODESTAB   = :pnCODESTAB
                           AND A.CODPRODUTO = :psCODPRODUTO";

            return await DbConn.FSVWProdutosDetalhes.FromSqlRaw(sql, 
                new OracleParameter("pnCODESTAB", OracleDbType.Int32, pCodEstab, ParameterDirection.Input),
                new OracleParameter("psCODPRODUTO", OracleDbType.Varchar2, pCodProduto, ParameterDirection.Input))
                .FirstOrDefaultAsync(cancellation);

        }
    }
}
