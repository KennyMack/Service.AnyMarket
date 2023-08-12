using Hino.Service.AnyMarket.Entities.Fiscal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_FSVWProdutosDetalhes : IEntityTypeConfiguration<FSVWProdutosDetalhes>
    {
        public void Configure(EntityTypeBuilder<FSVWProdutosDetalhes> builder)
        {
            builder.ToTable("FSVWPRODUTOSDETALHES");
            builder.HasKey(c => new
            {
                c.CODPRODUTO,
                c.CODESTAB
            });
            builder.Property(c => c.CODPRODUTO)
                .ValueGeneratedNever();
            builder.Property(c => c.CODESTAB);
            builder.Property(c => c.CODMARCA);
            builder.Property(c => c.DESCRICAO);
            builder.Property(c => c.FAMILIA);
            builder.Property(c => c.GRUPO);
            builder.Property(c => c.CLASSE);
            builder.Property(c => c.CATEGORIA);
            builder.Property(c => c.LARGURA);
            builder.Property(c => c.COMPRIMENTO);
            builder.Property(c => c.ALTURA);
            builder.Property(c => c.PESOBRUTO);
            builder.Property(c => c.CODORIGMERC);
            builder.Property(c => c.SALDOESTOQUE);
        }
    }
}