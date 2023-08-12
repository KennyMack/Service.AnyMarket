namespace Hino.Service.AnyMarket.Entities.Marketplace
{
    public class MPCategorias : BaseEntity
    {
        public long CODCONTROLE { get; set; }
        public short STATUSSINC { get; set; }
        public DateTime? DATASINC { get; set; }

        public string? CODFAMILIA { get; set; }
        public string? DESCFAMILIA { get; set; }
        public long? IDCATEGNV1 { get; set; }

        public string? CODGRUPO { get; set; }
        public string? DESCGRUPO { get; set; }
        public long? IDCATEGNV2 { get; set; }

        public string? CODCLASSE { get; set; }
        public string? DESCCLASSE { get; set; }
        public long? IDCATEGNV3 { get; set; }

        public string? CODCATEGORIA { get; set; }
        public string? DESCCATEGORIA { get; set; }
        public long? IDCATEGNV4 { get; set; }
    }
}
