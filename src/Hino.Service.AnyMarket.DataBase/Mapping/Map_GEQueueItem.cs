using Hino.Service.AnyMarket.Entities.CRM;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hino.Service.AnyMarket.Entities.General;
using static Hino.Service.AnyMarket.DataBase.ContextDB.ServiceContext;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_GEQueueItem : IEntityTypeConfiguration<GEQueueItem>
    {
        public void Configure(EntityTypeBuilder<GEQueueItem> builder)
        {
            builder.ToTable("GEQUEUEITEM");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID)
                .ValueGeneratedOnAdd()
                .HasValueGenerator((_, __) => new SequenceValueGenerator("SEQ_GEQUEUEITEM"));
            builder.Property(c => c.ESTABLISHMENTKEY);
            builder.Property(c => c.UNIQUEKEY);
            builder.Property(c => c.CREATED);
            builder.Property(c => c.MODIFIED);
            builder.Property(c => c.ISACTIVE);
            builder.Property(c => c.IDREFERENCE);
            builder.Property(c => c.PROCESSED);
            builder.Property(c => c.UPLOADED);
            builder.Property(c => c.ENTRYNAME);
            builder.Property(c => c.TYPE);
            builder.Property(c => c.NOTE);
            builder.Property(c => c.EXCEPTIONCODE);
        }
    }
}
