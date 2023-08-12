using Hino.Service.AnyMarket.DataBase.Mapping;
using Hino.Service.AnyMarket.Entities.CRM;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.General;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Hino.Service.AnyMarket.DataBase.ContextDB
{
    public class ServiceContext: DbContext
    {
        public DbSet<MPMarcas> MPMarcas { get; set; }
        public DbSet<MPProdAtributos> MPProdAtributos { get; set; }
        public DbSet<MPProdEstoque> MPProdEstoque { get; set; }
        public DbSet<MPProdImagens> MPProdImagens { get; set; }
        public DbSet<MPProdPreco> MPProdPreco { get; set; }
        public DbSet<MPProdutos> MPProdutos { get; set;  }
        public DbSet<MPCategorias> MPCategorias { get; set; }
        public DbSet<MPTiposVariacao> MPTiposVariacao { get; set; }
        public DbSet<MPTiposVarValores> MPTiposVarValores { get; set; }
        public DbSet<MPEstoque> MPEstoque { get; set; }

        public DbSet<CROrders> CROrders { get; set; }
        public DbSet<CROrderItems> CROrderItems { get; set; }
        public DbSet<GEQueueItem> GEQueueItem { get; set; }

        public DbSet<FSMarca> FSMarca { get; set; }
        public DbSet<FSFamiliaNiveis> FSFamiliaNiveis { get; set; }
        public DbSet<FSVWProdutosDetalhes> FSVWProdutosDetalhes { get; set; }
        public DbSet<FSVWProdutoSaldoEstoque> FSVWProdutoSaldoEstoque { get; set; }
        public DbSet<FSVWProdSaldoEstGrupoDisponivel> FSVWProdSaldoEstGrupoDisponivel { get; set; }


        public ServiceContext(DbContextOptions<ServiceContext> options) :
            base(options)
        {
            // this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false);
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configure ModelBuilder Entity Map
            modelBuilder.Entity<MPMarcas>(new Map_MPMarcas().Configure);
            modelBuilder.Entity<MPEstoque>(new Map_MPEstoque().Configure);
            modelBuilder.Entity<MPCategorias>(new Map_MPCategorias().Configure);
            modelBuilder.Entity<MPProdAtributos>(new Map_MPProdAtributos().Configure);
            modelBuilder.Entity<MPProdEstoque>(new Map_MPProdEstoque().Configure);
            modelBuilder.Entity<MPProdImagens>(new Map_MPProdImagens().Configure);
            modelBuilder.Entity<MPProdPreco>(new Map_MPProdPreco().Configure);
            modelBuilder.Entity<MPProdutos>(new Map_MPProdutos().Configure);
            modelBuilder.Entity<MPTiposVariacao>(new Map_MPTiposVariacao().Configure);
            modelBuilder.Entity<MPTiposVarValores>(new Map_MPTiposVarValores().Configure);
            modelBuilder.Entity<CROrders>(new Map_CROrders().Configure);
            modelBuilder.Entity<CROrderItems>(new Map_CROrderItems().Configure);
            modelBuilder.Entity<GEQueueItem>(new Map_GEQueueItem().Configure);


            modelBuilder.Entity<FSMarca>(new Map_FSMarca().Configure);
            modelBuilder.Entity<FSFamiliaNiveis>(new Map_FSFamiliaNiveis().Configure);
            modelBuilder.Entity<FSVWProdutosDetalhes>(new Map_FSVWProdutosDetalhes().Configure);
            modelBuilder.Entity<FSVWProdutoSaldoEstoque>(new Map_FSVWProdutoSaldoEstoque().Configure);
            modelBuilder.Entity<FSVWProdSaldoEstGrupoDisponivel>(new Map_FSVWProdSaldoEstGrupoDisponivel().Configure);
        }

        internal class SequenceValueGenerator : ValueGenerator<long>
        {
            private readonly string _sequenceName;

            public SequenceValueGenerator(string sequenceName)
            {
                _sequenceName = sequenceName;
            }

            public override bool GeneratesTemporaryValues => false;

            public override long Next(EntityEntry entry)
            {
                using (var command = entry.Context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = $"SELECT {_sequenceName}.NEXTVAL FROM DUAL";
                    entry.Context.Database.OpenConnection();
                    var reader = command.ExecuteScalar();
                    return Convert.ToInt32(reader);
                }
            }
        }
    }
}
