using Hino.Service.AnyMarket.Entities.Fiscal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Entities.Marketplace
{
    public class MPProdutos : BaseEntity
    {
        public MPProdutos()
        {
            ProdAtributos = new HashSet<MPProdAtributos>();
            ProdEstoque = new HashSet<MPProdEstoque>();
            ProdImagens = new HashSet<MPProdImagens>();
            ProdPreco = new HashSet<MPProdPreco>();
        }

        public long CODCONTROLE { get; set; }
        public DateTime DATAALTERACAO { get; set; }
        public string USUALTERACAO { get; set; }
        public short STATUSSINC { get; set; }
        public string? PROBLEMA { get; set; }
        public DateTime? DATASINC { get; set; }
        public short LIBERADOSINC { get; set; }
        public string CODPRODUTO { get; set; }
        public string CODGRUPOVARIACAO { get; set; }
        public int CODESTAB { get; set; }
        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        public string CATEGORIA { get; set; }
        public string CODBARRAS { get; set; }
        public string SKU { get; set; }
        public long? IDAPISKU { get; set; }
        public short GENERO { get; set; }
        public string? MODELO  { get; set; }
        public int? TEMPOGARANTIA  { get; set; }
        public string? AVISOGARANTIA { get; set; }
        public long? CODVARIACAOVLR { get; set; }
        public double ALTURA { get; set; }
        public double LARGURA { get; set; }
        public double COMPRIMENTO { get; set; }
        public double PESO { get; set; }
        public long? IDAPI { get; set; }

        public virtual ICollection<MPProdAtributos> ProdAtributos { get; set; }
        public virtual ICollection<MPProdEstoque> ProdEstoque { get; set; }
        public virtual ICollection<MPProdImagens> ProdImagens { get; set; }
        public virtual ICollection<MPProdPreco> ProdPreco { get; set; }
        public MPTiposVarValores ProdVariacaoVlr { get; set; }

        public FSVWProdutosDetalhes ProdDetalhes { get; set; }

        public string IsValidToUpload()
        {
            if (ProdDetalhes == null)
                return $"Não é possível enviar o produto: {CODPRODUTO} para a anymarket, Detalhes não encontrado na view 'FSVWPRODUTOSDETALHES'";

            if (ProdDetalhes.CATEGORIA == null ||
                ProdDetalhes.GRUPO == null ||
                ProdDetalhes.FAMILIA == null ||
                ProdDetalhes.CLASSE == null)
            {
                return $"Não é possível enviar o produto: {CODPRODUTO} para a anymarket, a arvore de categoria não está completa";
            }
            return null;
        }
    }
}
