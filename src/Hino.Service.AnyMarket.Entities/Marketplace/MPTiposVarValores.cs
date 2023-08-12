namespace Hino.Service.AnyMarket.Entities.Marketplace
{
    public class MPTiposVarValores : BaseEntity
    {
        public MPTiposVarValores()
        {
            Produto = new HashSet<MPProdutos>();
        }

        public long CODCONTROLE { get; set; }
        public long CODCTRLDET { get; set; }
        public string DESCRICAO { get; set; }
        public long? IDAPI { get; set; }

        public virtual ICollection<MPProdutos> Produto { get; set; }
        public virtual ICollection<MPProdImagens> ProdImagens { get; set; }
        public virtual MPTiposVariacao Tipo { get; set; }
    }
}
