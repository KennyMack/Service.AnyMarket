using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.Fiscal
{
    public class FSVWProdutosDetalhes
    {
        public string CODPRODUTO { get; set; }
        public int CODESTAB { get; set; }
        public long? CODMARCA { get; set; }
        public string? DESCRICAO { get; set; }
        public long? FAMILIA { get; set; }
        public long? GRUPO { get; set; }
        public long? CLASSE { get; set; }
        public long? CATEGORIA { get; set; }
        public double LARGURA { get; set; }
        public double COMPRIMENTO { get; set; }
        public double ALTURA { get; set; }
        public double PESOBRUTO { get; set; }
        public int? CODORIGMERC { get; set; }
        public double SALDOESTOQUE { get; set; }
    }
}
