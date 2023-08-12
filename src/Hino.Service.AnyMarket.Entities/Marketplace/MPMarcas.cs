using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.Marketplace
{
    public class MPMarcas : BaseEntity
    {
        public long CODCONTROLE { get; set; }
        public int CODMARCA { get; set; }
        public string DESCRICAO { get; set; }
        public short STATUSSINC { get; set; }
        public DateTime DATASINC { get; set; }
        public long? IDAPI { get; set; }
        public long? IDAPIPARTNER { get; set; }
    }
}
