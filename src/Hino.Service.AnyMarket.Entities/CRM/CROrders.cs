using Hino.Service.AnyMarket.Entities.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.CRM
{
    public class CROrders : BaseEntity
    {
        public CROrders()
        {
            this.VEOrderItems = new HashSet<CROrderItems>();
        }

        public long ID { get; set; }
        public string ESTABLISHMENTKEY { get; set; }
        public string UNIQUEKEY { get; set; }
        public DateTime CREATED { get; set; }
        public DateTime MODIFIED { get; set; }
        public bool ISACTIVE { get; set; }

        public long ENTERPRISEID { get; set; }
        public long? CARRIERID { get; set; }
        public long? REDISPATCHID { get; set; }
        public long USERID { get; set; }
        public long TYPEPAYMENTID { get; set; }
        public long PAYCONDITIONID { get; set; }
        public long CODPEDVENDA { get; set; }
        public long NUMPEDMOB { get; set; }
        public DateTime DELIVERYDATE { get; set; }
        public string NOTE { get; set; }
        public string INNERNOTE { get; set; }
        public string STATUS { get; set; }
        public string STATUSCRM { get; set; }
        public int STATUSSINC { get; set; }
        public bool ISPROPOSAL { get; set; }
        public int FREIGHTPAIDBY { get; set; }
        public int REDISPATCHPAIDBY { get; set; }
        public decimal FREIGHTVALUE { get; set; }
        public string CLIENTORDER { get; set; }

        public long IDERP { get; set; }
        public long? ORIGINORDERID { get; set; }
        public long? MAINORDERID { get; set; }
        public int ORDERVERSION { get; set; }

        public string CONTACTPHONE { get; set; }
        public string CONTACTEMAIL { get; set; }
        public string CONTACT { get; set; }
        public int SECTOR { get; set; }

        public long DIGITIZERID { get; set; }
        public decimal FINANCIALTAXES { get; set; }
        public bool ONLYONDATE { get; set; }
        public bool ALLOWPARTIAL { get; set; }
        public string REVISIONREASON { get; set; }

        public float PERCDISCOUNT { get; set; }
        public float PERCCOMMISSION { get; set; }

        public long QUEUEID { get; set; }
        public virtual GEQueueItem GEQUEUEITEM { get; set; }

        public bool INPERSON { get; set; }
        public DateTime? PAYMENTDUEDATE { get; set; }
        public int STATUSPAY { get; set; }
        public float PAIDAMOUNT { get; set; }

        public float ADVANCEAMOUNT { get; set; }
        public long? TYPESALEID { get; set; }

        public long? MESSAGEID { get; set; }

        public virtual ICollection<CROrderItems> VEOrderItems { get; set; }
    }
}
