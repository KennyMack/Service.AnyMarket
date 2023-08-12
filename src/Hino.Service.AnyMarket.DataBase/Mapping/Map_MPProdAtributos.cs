using Hino.Service.AnyMarket.Entities.Marketplace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Hino.Service.AnyMarket.DataBase.ContextDB.ServiceContext;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_MPProdAtributos : IEntityTypeConfiguration<MPProdAtributos>
    {
        public void Configure(EntityTypeBuilder<MPProdAtributos> builder)
        {
            builder.ToTable("MPPRODATRIBUTOS");
            builder.HasKey(c => c.CODCONTROLE);
            builder.Property(c => c.CODCONTROLE)
                .ValueGeneratedOnAdd()
                .HasValueGenerator((_, __) => new SequenceValueGenerator("SEQ_MPPRODATRIBUTOS"));

            builder.Property(c => c.CODCTRLDET);
            builder.Property(c => c.CARACTERISTICA);
            builder.Property(c => c.VALOR);


            builder.HasOne(c => c.Produto)
                .WithMany(x => x.ProdAtributos)
                .HasForeignKey(k => k.CODCONTROLE);
        }
    }
}
