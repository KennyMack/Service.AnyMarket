using Hino.Service.AnyMarket.Entities.CRM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Hino.Service.AnyMarket.DataBase.ContextDB.ServiceContext;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_CROrderItems : IEntityTypeConfiguration<CROrderItems>
    {
        public void Configure(EntityTypeBuilder<CROrderItems> builder)
        {
            builder.ToTable("CRORDERITEMS");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID)
                .ValueGeneratedOnAdd()
                .HasValueGenerator((_, __) => new SequenceValueGenerator("SEQ_CRORDERITEMS"));
            builder.Property(c => c.ESTABLISHMENTKEY);
            builder.Property(c => c.UNIQUEKEY);
            builder.Property(c => c.CREATED);
            builder.Property(c => c.MODIFIED);
            builder.Property(c => c.ISACTIVE);
            builder.Property(c => c.ORDERID);
            builder.Property(c => c.PRODUCTID);
            builder.Property(c => c.FISCALOPERID);
            builder.Property(c => c.TABLEVALUE)
                .HasColumnType("NUMBER");
            builder.Property(c => c.VALUE);
            builder.Property(c => c.QUANTITY);
            builder.Property(c => c.QUANTITYREFERENCE);
            builder.Property(c => c.PERCDISCOUNT);
            builder.Property(c => c.NOTE);
            builder.Property(c => c.ITEM);
            builder.Property(c => c.ITEMLEVEL);
            builder.Property(c => c.IDERP);
            builder.Property(c => c.CLIENTORDER);
            builder.Property(c => c.CLIENTITEM);
            builder.Property(c => c.DELIVERYDATE);
            builder.Property(c => c.PERCDISCOUNTHEAD);
            builder.Property(c => c.PERCCOMMISSION);
            builder.Property(c => c.PERCCOMMISSIONHEAD);
            builder.Property(c => c.SHIPPINGDAYS);
            builder.Property(c => c.ALTDESCRIPTION);
        }
    }
}
