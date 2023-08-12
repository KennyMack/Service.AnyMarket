using Hino.Service.AnyMarket.Entities.Marketplace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Hino.Service.AnyMarket.DataBase.ContextDB.ServiceContext;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_MPTiposVarValores : IEntityTypeConfiguration<MPTiposVarValores>
    {
        public void Configure(EntityTypeBuilder<MPTiposVarValores> builder)
        {
            builder.ToTable("MPTIPOSVARVALORES");
            builder.HasKey(c => c.CODCONTROLE);
            builder.Property(c => c.CODCONTROLE)
                .ValueGeneratedOnAdd()
                .HasValueGenerator((_, __) => new SequenceValueGenerator("SEQ_MPPRODATRIBUTOS"));

            builder.Property(c => c.CODCTRLDET);
            builder.Property(c => c.DESCRICAO);
            builder.Property(c => c.IDAPI);
        }
    }
}
