using Hino.Service.AnyMarket.Entities.Marketplace;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hino.Service.AnyMarket.DataBase.ContextDB.ServiceContext;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_MPProdutos : IEntityTypeConfiguration<MPProdutos>
    {
        public void Configure(EntityTypeBuilder<MPProdutos> builder)
        {
            builder.ToTable("MPPRODUTOS");
            builder.HasKey(c => c.CODCONTROLE);
            builder.Property(c => c.CODCONTROLE)
                .ValueGeneratedOnAdd()
                .HasValueGenerator((_, __) => new SequenceValueGenerator("SEQ_MPPRODUTOS"));
            builder.Property(c => c.DATAALTERACAO);
            builder.Property(c => c.USUALTERACAO);
            builder.Property(c => c.PROBLEMA);
            builder.Property(c => c.STATUSSINC);
            builder.Property(c => c.DATASINC);
            builder.Property(c => c.LIBERADOSINC);
            builder.Property(c => c.CODPRODUTO);
            builder.Property(c => c.CODGRUPOVARIACAO);
            builder.Property(c => c.CODESTAB);
            builder.Property(c => c.TITULO);
            builder.Property(c => c.DESCRICAO);
            builder.Property(c => c.CATEGORIA);
            builder.Property(c => c.CODBARRAS);
            builder.Property(c => c.SKU);
            builder.Property(c => c.IDAPISKU);
            builder.Property(c => c.GENERO);
            builder.Property(c => c.MODELO);
            builder.Property(c => c.TEMPOGARANTIA);
            builder.Property(c => c.AVISOGARANTIA);
            builder.Property(c => c.CODVARIACAOVLR);
            builder.Property(c => c.ALTURA);
            builder.Property(c => c.LARGURA);
            builder.Property(c => c.COMPRIMENTO);
            builder.Property(c => c.PESO);
            builder.Property(c => c.IDAPI);

            builder.Ignore(c => c.ProdDetalhes);

            builder.HasOne(c => c.ProdVariacaoVlr)
                .WithMany(x => x.Produto)
                .HasForeignKey(s => s.CODVARIACAOVLR)
                .IsRequired(false);

            // builder.HasOne(c => c.ProdVariacaoVlr)
            //     .WithMany(c => c.Produto);

            builder.HasMany(c => c.ProdAtributos)
                .WithOne(x => x.Produto)
                .HasForeignKey(k => k.CODCTRLDET);

            builder.HasMany(c => c.ProdEstoque)
                .WithOne(x => x.Produto)
                .HasForeignKey(k => k.CODCTRLDET);

            builder.HasMany(c => c.ProdImagens)
                .WithOne(x => x.Produto)
                .HasForeignKey(k => k.CODCTRLDET);

            builder.HasMany(c => c.ProdPreco)
                .WithOne(x => x.Produto)
                .HasForeignKey(k => k.CODCTRLDET);

        }
    }
}
