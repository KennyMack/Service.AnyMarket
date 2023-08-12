using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.Marketplace
{
    public class MPProdAtributos : BaseEntity
    {
        public long CODCONTROLE { get; set; }
        public long CODCTRLDET { get; set; }
        public string CARACTERISTICA { get; set; }
        public string VALOR { get; set; }

        public MPProdutos Produto { get; set; }
    }
}
