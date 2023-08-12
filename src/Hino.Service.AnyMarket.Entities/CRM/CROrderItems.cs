using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.CRM
{
    public class CROrderItems : BaseEntity
    {
        public long ID { get; set; }
        public string ESTABLISHMENTKEY { get; set; }
        public string UNIQUEKEY { get; set; }
        public DateTime CREATED { get; set; }
        public DateTime MODIFIED { get; set; }
        public bool ISACTIVE { get; set; }
        public long ORDERID { get; set; }
        public virtual CROrders CRORDERS { get; set; }
        public long PRODUCTID { get; set; }
        public long FISCALOPERID { get; set; }
        public decimal TABLEVALUE { get; set; }
        public float VALUE { get; set; }
        public float QUANTITY { get; set; }
        public float QUANTITYREFERENCE { get; set; }
        public float PERCDISCOUNT { get; set; }
        public string NOTE { get; set; }
        public int ITEM { get; set; }
        public int ITEMLEVEL { get; set; }
        public long IDERP { get; set; }
        public string CLIENTORDER { get; set; }
        public string CLIENTITEM { get; set; }
        public DateTime DELIVERYDATE { get; set; }
        public float PERCDISCOUNTHEAD { get; set; }
        public float PERCCOMMISSION { get; set; }
        public float PERCCOMMISSIONHEAD { get; set; }
        public int SHIPPINGDAYS { get; set; }
        public bool ALTDESCRIPTION { get; set; }
    }
}
