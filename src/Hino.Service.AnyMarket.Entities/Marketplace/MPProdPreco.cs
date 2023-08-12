using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.Marketplace
{
    public class MPProdPreco : BaseEntity
    {
        public long CODCONTROLE { get; set; }
        public long CODCTRLDET { get; set; }
        public int CODTIPOVENDA { get; set; }
        public int CODESTAB { get; set; }
        public int CODPRVENDA { get; set; }
        public string CODPRODUTO { get; set; }
        public int CODREGIAO { get; set; }
        public decimal VALORUNITARIO { get; set; }

        public virtual MPProdutos Produto { get; set; }
    }
}
