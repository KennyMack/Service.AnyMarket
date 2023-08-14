using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.General
{
    public class GEQueueItem: BaseEntity
    {
        public long ID { get; set; }
        public string ESTABLISHMENTKEY { get; set; }
        public string UNIQUEKEY { get; set; }
        public DateTime CREATED { get; set; }
        public DateTime MODIFIED { get; set; }
        public bool ISACTIVE { get; set; }
        public long IDREFERENCE { get; set; }
        public bool PROCESSED { get; set; }
        public bool UPLOADED { get; set; }
        public string ENTRYNAME { get; set; }
        public string TYPE { get; set; }
        public string? NOTE { get; set; }
        public string? EXCEPTIONCODE { get; set; }

    }
}
