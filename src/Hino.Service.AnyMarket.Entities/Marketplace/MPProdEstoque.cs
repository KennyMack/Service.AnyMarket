using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.Marketplace
{
    public class MPProdEstoque : BaseEntity
    {
        public long CODCONTROLE { get; set; }
        public long CODCTRLDET { get; set; }
        public string CODPRODUTO { get; set; }
        public int CODESTAB { get; set; }
        public string CODESTOQUE { get; set; }

        public virtual MPProdutos Produto { get; set; }
    }
}
