using Hino.Service.AnyMarket.Entities.Fiscal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_FSFamiliaNiveis : IEntityTypeConfiguration<FSFamiliaNiveis>
    {
        public void Configure(EntityTypeBuilder<FSFamiliaNiveis> builder)
        {
            builder.HasNoKey();
            builder.Property(c => c.CODFAMILIA);
            builder.Property(c => c.DESCRICAOFAMILIA);
            builder.Property(c => c.CODGRUPO);
            builder.Property(c => c.DESCRICAOGRUPO);
            builder.Property(c => c.CODCLASSE);
            builder.Property(c => c.DESCRICAOCLASSE);
            builder.Property(c => c.CODCATEGORIA);
            builder.Property(c => c.DESCRICAOCATEGORIA);
        }
    }
}
