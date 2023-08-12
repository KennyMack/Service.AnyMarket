using Hino.Service.AnyMarket.Entities.Fiscal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hino.Service.AnyMarket.Entities.CRM;
using static Hino.Service.AnyMarket.DataBase.ContextDB.ServiceContext;

namespace Hino.Service.AnyMarket.DataBase.Mapping
{
    internal class Map_CROrders : IEntityTypeConfiguration<CROrders>
    {
        public void Configure(EntityTypeBuilder<CROrders> builder)
        {
            builder.ToTable("CRORDERS");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID)
                .ValueGeneratedOnAdd()
                .HasValueGenerator((_, __) => new SequenceValueGenerator("SEQ_CRORDERS"));
            builder.Property(c => c.ESTABLISHMENTKEY);
            builder.Property(c => c.UNIQUEKEY);
            builder.Property(c => c.CREATED);
            builder.Property(c => c.MODIFIED);
            builder.Property(c => c.ISACTIVE);
            builder.Property(c => c.ENTERPRISEID);
            builder.Property(c => c.CARRIERID);
            builder.Property(c => c.REDISPATCHID);
            builder.Property(c => c.USERID);
            builder.Property(c => c.TYPEPAYMENTID);
            builder.Property(c => c.PAYCONDITIONID);
            builder.Property(c => c.CODPEDVENDA);
            builder.Property(c => c.NUMPEDMOB);
            builder.Property(c => c.DELIVERYDATE);
            builder.Property(c => c.NOTE);
            builder.Property(c => c.INNERNOTE);
            builder.Property(c => c.STATUS);
            builder.Property(c => c.STATUSCRM);
            builder.Property(c => c.STATUSSINC);
            builder.Property(c => c.ISPROPOSAL);
            builder.Property(c => c.FREIGHTPAIDBY);
            builder.Property(c => c.REDISPATCHPAIDBY);
            builder.Property(c => c.FREIGHTVALUE);
            builder.Property(c => c.CLIENTORDER);
            builder.Property(c => c.IDERP);
            builder.Property(c => c.ORIGINORDERID);
            builder.Property(c => c.MAINORDERID);
            builder.Property(c => c.ORDERVERSION);
            builder.Property(c => c.CONTACTPHONE);
            builder.Property(c => c.CONTACTEMAIL);
            builder.Property(c => c.CONTACT);
            builder.Property(c => c.SECTOR);
            builder.Property(c => c.DIGITIZERID);
            builder.Property(c => c.FINANCIALTAXES);
            builder.Property(c => c.ONLYONDATE);
            builder.Property(c => c.ALLOWPARTIAL);
            builder.Property(c => c.REVISIONREASON);
            builder.Property(c => c.PERCDISCOUNT);
            builder.Property(c => c.PERCCOMMISSION);
            builder.Property(c => c.QUEUEID);
            builder.Property(c => c.GEQUEUEITEM);
            builder.Property(c => c.INPERSON);
            builder.Property(c => c.PAYMENTDUEDATE);
            builder.Property(c => c.STATUSPAY);
            builder.Property(c => c.PAIDAMOUNT);
            builder.Property(c => c.ADVANCEAMOUNT);
            builder.Property(c => c.TYPESALEID);
            builder.Property(c => c.MESSAGEID);

            builder.HasMany(c => c.VEOrderItems)
                .WithOne(x => x.CRORDERS)
                .HasForeignKey(k => k.ORDERID);
        }
    }
}
