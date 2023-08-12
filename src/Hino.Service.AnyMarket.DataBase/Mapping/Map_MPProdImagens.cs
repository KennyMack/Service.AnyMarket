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
    internal class Map_MPProdImagens : IEntityTypeConfiguration<MPProdImagens>
    {
        public void Configure(EntityTypeBuilder<MPProdImagens> builder)
        {
            builder.ToTable("MPPRODIMAGENS");
            builder.HasKey(c => c.CODCONTROLE);
            builder.Property(c => c.CODCONTROLE)
                .ValueGeneratedOnAdd()
                .HasValueGenerator((_, __) => new SequenceValueGenerator("SEQ_MPPRODIMAGENS"));
            builder.Property(c => c.CODCTRLDET);
            builder.Property(c => c.URL);
            builder.Property(c => c.PRINCIPAL);
            builder.Property(c => c.INDICE);
            builder.Property(c => c.CODVARIACAOVLR);
            builder.Property(c => c.IDAPI);
            builder.Property(c => c.EXCLUIDO);

            builder.HasOne(c => c.ProdVariacaoVlr)
                .WithMany(x => x.ProdImagens)
                .HasForeignKey(s => s.CODVARIACAOVLR)
                .IsRequired(false);
        }
    }
}
