using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Marcas;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Logs;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Marcas
{
    public class MPMarcasDownloadService : IMPMarcasDownloadService
    {
        public List<string> Errors { get; set; }

        readonly IMPMarcasRepository MPMarcasRepository;

        public MPMarcasDownloadService(IMPMarcasRepository pRepository)
        {
            Errors = new List<string>();
            MPMarcasRepository = pRepository;
        }

        public async Task UpdateERPAsync(CancellationToken cancellation, List<MPMarcas> pMarcas)
        {
            foreach (var item in pMarcas)
            {
                try
                {
                    var OldMarca = await MPMarcasRepository.GetByKeyAsync(cancellation, r => r.CODCONTROLE == item.CODCONTROLE);

                    if (OldMarca == null)
                    {
                        MPMarcasRepository.Add(new MPMarcas
                        {
                            CODCONTROLE = 0,
                            CODMARCA = item.CODMARCA,
                            DATASINC = DateTime.Now,
                            STATUSSINC = 1,
                            DESCRICAO = item.DESCRICAO,
                            IDAPI = item.IDAPI,
                            IDAPIPARTNER = item.IDAPIPARTNER,
                        });
                    }
                    else
                    {
                        OldMarca.STATUSSINC = 1;
                        OldMarca.IDAPI = item.IDAPI;
                        OldMarca.IDAPIPARTNER = item.IDAPIPARTNER;
                        OldMarca.DATASINC = DateTime.Now;

                        MPMarcasRepository.Update(OldMarca);
                    }

                    await MPMarcasRepository.SaveChangesAsync(cancellation);
                }
                catch (Exception ex)
                {
                    var ms = $"Não foi possível atualizar o ERP, da marca CODCONTROLE: {item.CODCONTROLE} e CODMARCA: {item.CODMARCA}";
                    Errors.Add(ms);
                    Logger.LogError(ms, ex);
                }
            }
        }

        public void Dispose()
        {
            MPMarcasRepository?.Dispose();
        }

    }
}
