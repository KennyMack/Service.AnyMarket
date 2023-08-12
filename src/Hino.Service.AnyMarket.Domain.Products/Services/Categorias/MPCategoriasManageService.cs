using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Categorias;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Logs;
using Hino.Service.AnyMarket.Utils;
using NetSwissTools.Utils;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Categorias
{
    public class MPCategoriasManageService : IMPCategoriasManageService
    {
        public List<string> Errors { get; set; }

        readonly IMPCategoriasRepository MPCategoriasRepository;
        readonly IMPCategoriasReaderService MPCategoriasReaderService;

        public MPCategoriasManageService(
            IMPCategoriasRepository pRepository,
            IMPCategoriasReaderService pReaderService)
        {
            Errors = new List<string>();
            MPCategoriasReaderService = pReaderService;
            MPCategoriasRepository = pRepository;
        }

        public async Task<IEnumerable<MPCategorias>> GetCategoriasToUploadAsync(CancellationToken cancellation)
        {
            var CategoriasAPI = await MPCategoriasReaderService.GetCategoriasAPIAsync(cancellation);
            var CategoriasERP = await MPCategoriasReaderService.GetCategoriasERPAsync(cancellation);

            var CategoriasRetorno = new List<MPCategorias>();
            foreach (var Categoria in CategoriasERP)
            {
                var OldCategoria = CategoriasAPI.Where(x =>
                    x.CODFAMILIA == Categoria.CODFAMILIA &&
                    x.CODGRUPO == Categoria.CODGRUPO &&
                    x.CODCLASSE == Categoria.CODCLASSE &&
                    x.CODCATEGORIA == Categoria.CODCATEGORIA);

                var mpCategoria = CategoriasAPI.FirstOrDefault(x =>
                    x.CODCATEGORIA == Categoria.CODCATEGORIA);
                var mpClasse = CategoriasAPI.FirstOrDefault(x =>
                    x.CODCLASSE == Categoria.CODCLASSE);
                var mpGrupo = CategoriasAPI.FirstOrDefault(x =>
                    x.CODGRUPO == Categoria.CODGRUPO);
                var mpFamilia = CategoriasAPI.FirstOrDefault(x =>
                    x.CODFAMILIA == Categoria.CODFAMILIA);

                var NewCategoria = new MPCategorias
                {
                    CODCONTROLE = 0,
                    DATASINC = DateTime.Now,
                    STATUSSINC = 0,

                    CODFAMILIA = Categoria.CODFAMILIA,
                    DESCFAMILIA = (Categoria.DESCRICAOFAMILIA ?? "").ToLower()
                        .ClearSpecialChars().CapitalizeFirst(),
                    IDCATEGNV1 = mpCategoria?.IDCATEGNV1,

                    CODGRUPO = Categoria.CODGRUPO,
                    DESCGRUPO = (Categoria.DESCRICAOGRUPO ?? "").ToLower()
                        .ClearSpecialChars().CapitalizeFirst(),
                    IDCATEGNV2 = mpGrupo?.IDCATEGNV2,

                    CODCLASSE = Categoria.CODCLASSE,
                    DESCCLASSE = (Categoria.DESCRICAOCLASSE ?? "").ToLower()
                        .ClearSpecialChars().CapitalizeFirst(),
                    IDCATEGNV3 = mpClasse?.IDCATEGNV3,

                    CODCATEGORIA = Categoria.CODCATEGORIA,
                    DESCCATEGORIA = (Categoria.DESCRICAOCATEGORIA ?? "").ToLower()
                        .ClearSpecialChars().CapitalizeFirst(),
                    IDCATEGNV4 = mpCategoria?.IDCATEGNV4
                };

                if (OldCategoria.Any())
                {
                    var CatF = OldCategoria.First();
                    NewCategoria.CODCONTROLE = CatF.CODCONTROLE;
                }

                CategoriasRetorno.Add(NewCategoria);
            }
            return CategoriasRetorno;
        }

        public async Task<bool> GenerateCategoriasAsync(CancellationToken cancellation, List<MPCategorias> pCategorias)
        {
            try
            {
                MPCategoriasRepository.UpdateCategorias(pCategorias.Where(x => x.CODCONTROLE > 0).ToList());
                MPCategoriasRepository.AddCategorias(pCategorias.Where(x => x.CODCONTROLE == 0).ToList());

                var rows = await MPCategoriasRepository.SaveChangesAsync(cancellation);
                return rows > 0;
            }
            catch (Exception ex)
            {
                var msg = "Não foi possível gerar a lista de categorias do erp";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return false;
            }
        }

        public void Dispose()
        {
            MPCategoriasRepository?.Dispose();
            MPCategoriasReaderService?.Dispose();
        }
    }
}
