using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.Marketplace
{
    public class MPEstoque : BaseEntity
    {
        public long CODCONTROLE { get; set; }
        public string DESCRICAO { get; set; }
        public bool VIRTUAL { get; set; }
        public bool PADRAO { get; set; }
        public long? IDAPI { get; set; }
    }
}
