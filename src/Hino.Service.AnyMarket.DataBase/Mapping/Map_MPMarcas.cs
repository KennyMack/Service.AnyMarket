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
    internal class Map_MPMarcas : IEntityTypeConfiguration<MPMarcas>
    {
        public void Configure(EntityTypeBuilder<MPMarcas> builder)
        {
            builder.ToTable("MPMARCAS");
            builder.HasKey(c => c.CODCONTROLE);
            builder.Property(c => c.CODCONTROLE)
                .ValueGeneratedOnAdd()
                .HasValueGenerator((_, __) => new SequenceValueGenerator("SEQ_MPMARCAS"));
            builder.Property(c => c.CODMARCA);
            builder.Property(c => c.DESCRICAO);
            builder.Property(c => c.STATUSSINC);
            builder.Property(c => c.DATASINC);
            builder.Property(c => c.IDAPI);
            builder.Property(c => c.IDAPIPARTNER);
        }
    }
}
