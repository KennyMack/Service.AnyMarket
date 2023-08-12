using Hino.Service.AnyMarket.Entities.Marketplace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Hino.Service.AnyMarket.DataBase.ContextDB.ServiceContext;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_MPTiposVariacao : IEntityTypeConfiguration<MPTiposVariacao>
    {
        public void Configure(EntityTypeBuilder<MPTiposVariacao> builder)
        {
            builder.ToTable("MPTIPOSVARIACAO");
            builder.HasKey(c => c.CODCONTROLE);
            builder.Property(c => c.CODCONTROLE)
                .ValueGeneratedOnAdd()
                .HasValueGenerator((_, __) => new SequenceValueGenerator("SEQ_MPPRODATRIBUTOS"));

            builder.Property(c => c.NOME);
            builder.Property(c => c.IDAPI);
            builder.Property(c => c.VARIACAOVISUAL);

            builder.HasMany(c => c.Valores)
                .WithOne(x => x.Tipo)
                .HasForeignKey(k => k.CODCTRLDET);
        }
    }
}
