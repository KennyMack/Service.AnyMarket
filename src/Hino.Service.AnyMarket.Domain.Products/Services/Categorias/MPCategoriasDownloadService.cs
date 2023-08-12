using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Categorias;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Logs;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Categorias
{
    public class MPCategoriasDownloadService: IMPCategoriasDownloadService
    {
        public List<string> Errors { get; set; }

        readonly IMPCategoriasRepository MPCategoriasRepository;

        public MPCategoriasDownloadService(IMPCategoriasRepository pRepository)
        {
            Errors = new List<string>();
            MPCategoriasRepository = pRepository;
        }

        public async Task UpdateERPAsync(CancellationToken cancellation, List<MPCategorias> pMarcas)
        {
            foreach (var item in pMarcas)
            {
                try
                {
                    var OldMarca = await MPCategoriasRepository.GetByKeyAsync(cancellation, r => r.CODCONTROLE == item.CODCONTROLE);

                    if (OldMarca == null)
                    {
                        MPCategoriasRepository.Add(new MPCategorias
                        {
                            CODCONTROLE = 0,
                            DATASINC = DateTime.Now,
                            STATUSSINC = 1,

                            CODFAMILIA = item.CODFAMILIA,
                            DESCFAMILIA = item.DESCFAMILIA,
                            IDCATEGNV1 = item.IDCATEGNV1,

                            CODGRUPO = item.CODGRUPO,
                            DESCGRUPO = item.DESCGRUPO,
                            IDCATEGNV2 = item.IDCATEGNV2,

                            CODCLASSE = item.CODCLASSE,
                            DESCCLASSE = item.DESCCLASSE,
                            IDCATEGNV3 = item.IDCATEGNV3,

                            CODCATEGORIA = item.CODCATEGORIA,
                            DESCCATEGORIA = item.DESCCATEGORIA,
                            IDCATEGNV4 = item.IDCATEGNV4
                        });
                    }
                    else
                    {
                        OldMarca.STATUSSINC = 1;
                        OldMarca.DATASINC = DateTime.Now;

                        OldMarca.CODFAMILIA = item.CODFAMILIA;
                        OldMarca.DESCFAMILIA = item.DESCFAMILIA;
                        OldMarca.IDCATEGNV1 = item.IDCATEGNV1;

                        OldMarca.CODGRUPO = item.CODGRUPO;
                        OldMarca.DESCGRUPO = item.DESCGRUPO;
                        OldMarca.IDCATEGNV2 = item.IDCATEGNV2;

                        OldMarca.CODCLASSE = item.CODCLASSE;
                        OldMarca.DESCCLASSE = item.DESCCLASSE;
                        OldMarca.IDCATEGNV3 = item.IDCATEGNV3;

                        OldMarca.CODCATEGORIA = item.CODCATEGORIA;
                        OldMarca.DESCCATEGORIA = item.DESCCATEGORIA;
                        OldMarca.IDCATEGNV4 = item.IDCATEGNV4;

                        MPCategoriasRepository.Update(OldMarca);
                    }

                    await MPCategoriasRepository.SaveChangesAsync(cancellation);
                }
                catch (Exception ex)
                {
                    var ms = $"Não foi possível atualizar o ERP, da categoria CODCONTROLE: {item.CODCONTROLE} e CODFAMILIA: {item.CODFAMILIA}";
                    Errors.Add(ms);
                    Logger.LogError(ms, ex);
                }
            }
        }

        public void Dispose()
        {
            MPCategoriasRepository?.Dispose();
        }
    }
}
