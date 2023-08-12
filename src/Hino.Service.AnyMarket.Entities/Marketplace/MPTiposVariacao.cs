namespace Hino.Service.AnyMarket.Entities.Marketplace
{
    public class MPTiposVariacao : BaseEntity
    {
        public MPTiposVariacao()
        {
            Valores = new HashSet<MPTiposVarValores>();
        }

        public long CODCONTROLE { get; set; }
        public string NOME { get; set; }
        public long? IDAPI { get; set; }
        public bool VARIACAOVISUAL { get; set; }

        public virtual ICollection<MPTiposVarValores> Valores { get; set; }
    }
}

