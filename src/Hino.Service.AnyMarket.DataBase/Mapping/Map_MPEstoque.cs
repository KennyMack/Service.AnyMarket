using Hino.Service.AnyMarket.Entities.Marketplace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_MPEstoque : IEntityTypeConfiguration<MPEstoque>
    {
        public void Configure(EntityTypeBuilder<MPEstoque> builder)
        {
            builder.ToTable("MPESTOQUE");
            builder.HasKey(c => c.CODCONTROLE);
            builder.Property(c => c.CODCONTROLE);
            builder.Property(c => c.DESCRICAO);
            builder.Property(c => c.VIRTUAL);
            builder.Property(c => c.PADRAO);
            builder.Property(c => c.IDAPI);
        }
    }
}
