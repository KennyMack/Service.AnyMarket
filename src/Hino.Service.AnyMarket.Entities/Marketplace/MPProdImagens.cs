using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.Marketplace
{
    public class MPProdImagens : BaseEntity
    {
        public long CODCONTROLE { get; set; }
        public long CODCTRLDET { get; set; }
        public string URL { get; set; }
        public short PRINCIPAL { get; set; }
        public long INDICE { get; set; }
        public long? CODVARIACAOVLR { get; set; }
        public long? IDAPI { get; set; }
        public bool EXCLUIDO { get; set; }

        public virtual MPTiposVarValores ProdVariacaoVlr { get; set; }
        public virtual MPProdutos Produto { get; set; }
    }
}
