namespace Hino.Service.AnyMarket.Entities.Fiscal
{
    public class FSVWProdSaldoEstGrupoDisponivel: BaseEntity
    {
        public int CODESTAB { get; set; }
        public string CODGRUPOESTOQ { get; set; }
        public string CODPRODUTO { get; set; }
        public long? IDAPIESTOQUE { get; set; }
        public long? IDAPIPRODUTO { get; set; }
        public long? IDAPISKU { get; set; }
        public double SALDODISPONIVEL { get; set; }
        public double SALDOESTOQUE { get; set; }
        public double CUSTO { get; set; }
        public int LEADTIME { get; set; }
        public int STATUS { get; set; }
    }
}
