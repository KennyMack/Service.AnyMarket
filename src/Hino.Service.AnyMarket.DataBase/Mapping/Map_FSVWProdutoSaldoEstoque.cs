using Hino.Service.AnyMarket.Entities.Fiscal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_FSVWProdutoSaldoEstoque : IEntityTypeConfiguration<FSVWProdutoSaldoEstoque>
    {
        public void Configure(EntityTypeBuilder<FSVWProdutoSaldoEstoque> builder)
        {
            builder.ToTable("FSVWPRODUTOSALDOESTOQUE");
            builder.HasKey(c => new
            {
                c.CODPRODUTO,
                c.CODESTAB
            });
            builder.Property(c => c.CODPRODUTO)
                .ValueGeneratedNever();
            builder.Property(c => c.CODESTAB);
            builder.Property(c => c.CODGRUPOESTOQ);
            builder.Property(c => c.CODPRODUTO);
            builder.Property(c => c.IDAPIESTOQUE);
            builder.Property(c => c.IDAPIPRODUTO);
            builder.Property(c => c.IDAPISKU);
            builder.Property(c => c.SALDODISPONIVEL);
            builder.Property(c => c.SALDOESTOQUE);
            builder.Property(c => c.STATUS);
        }
    }
}
