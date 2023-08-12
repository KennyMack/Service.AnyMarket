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
    internal class Map_MPProdPreco : IEntityTypeConfiguration<MPProdPreco>
    {
        public void Configure(EntityTypeBuilder<MPProdPreco> builder)
        {
            builder.ToTable("MPPRODPRECO");
            builder.HasKey(c => c.CODCONTROLE);
            builder.Property(c => c.CODCONTROLE)
                .ValueGeneratedOnAdd()
                .HasValueGenerator((_, __) => new SequenceValueGenerator("SEQ_MPPRODPRECO"));
            builder.Property(c => c.CODCTRLDET);
            builder.Property(c => c.CODTIPOVENDA);
            builder.Property(c => c.CODESTAB);
            builder.Property(c => c.CODPRVENDA);
            builder.Property(c => c.CODPRODUTO);
            builder.Property(c => c.CODREGIAO);
            builder.Property(c => c.VALORUNITARIO);
        }
    }
}
