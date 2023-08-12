using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.Fiscal
{
    public class FSVWProdutoSaldoEstoque: BaseEntity
    {
        public int CODESTAB { get; set; }
        public string CODGRUPOESTOQ { get; set; }
        public string CODPRODUTO { get; set; }
        public long? IDAPIESTOQUE { get; set; }
        public long? IDAPIPRODUTO { get; set; }
        public long? IDAPISKU { get; set; }
        public double SALDODISPONIVEL { get; set; }
        public double SALDOESTOQUE { get; set; }
        public int STATUS { get; set; }
    }
}




