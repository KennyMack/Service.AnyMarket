using Hino.Service.AnyMarket.Entities.Fiscal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hino.Service.AnyMarket.Entities.Marketplace;
using static Hino.Service.AnyMarket.DataBase.ContextDB.ServiceContext;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_MPCategorias : IEntityTypeConfiguration<MPCategorias>
    {
        public void Configure(EntityTypeBuilder<MPCategorias> builder)
        {
            builder.ToTable("MPCATEGORIAS");
            builder.HasKey(c => c.CODCONTROLE);
            builder.Property(c => c.CODCONTROLE)
                .ValueGeneratedOnAdd()
                .HasValueGenerator((_, __) => new SequenceValueGenerator("SEQ_MPCATEGORIAS"));
            builder.Property(c => c.STATUSSINC);
            builder.Property(c => c.DATASINC);
            builder.Property(c => c.CODFAMILIA);
            builder.Property(c => c.DESCFAMILIA);
            builder.Property(c => c.IDCATEGNV1);
            builder.Property(c => c.CODGRUPO);
            builder.Property(c => c.DESCGRUPO);
            builder.Property(c => c.IDCATEGNV2);
            builder.Property(c => c.CODCLASSE);
            builder.Property(c => c.DESCCLASSE);
            builder.Property(c => c.IDCATEGNV3);
            builder.Property(c => c.CODCATEGORIA);
            builder.Property(c => c.DESCCATEGORIA);
            builder.Property(c => c.IDCATEGNV4);

            builder.HasIndex(c => c.STATUSSINC);
        }
    }
}
