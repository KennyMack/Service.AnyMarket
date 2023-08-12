using Hino.Service.AnyMarket.Entities.Marketplace;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hino.Service.AnyMarket.Entities.Fiscal;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_FSMarca : IEntityTypeConfiguration<FSMarca>
    {
        public void Configure(EntityTypeBuilder<FSMarca> builder)
        {
            builder.ToTable("FSMARCA");
            builder.HasKey(c => c.CODMARCA);
            builder.Property(c => c.CODMARCA);
            builder.Property(c => c.DESCRICAO);
        }
    }
}
