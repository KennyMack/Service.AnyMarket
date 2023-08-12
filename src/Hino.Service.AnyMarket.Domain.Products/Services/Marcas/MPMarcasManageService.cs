using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Marcas;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Logs;
using NetSwissTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Marcas
{
    public class MPMarcasManageService : IMPMarcasManageService
    {
        public List<string> Errors { get; set; }

        readonly IMPMarcasRepository MPMarcasRepository;
        readonly IMPMarcasReaderService MPMarcasReaderService;

        public MPMarcasManageService(
            IMPMarcasRepository pRepository,
            IMPMarcasReaderService pReaderService)
        {
            Errors = new List<string>();
            MPMarcasReaderService = pReaderService;
            MPMarcasRepository = pRepository;
        }

        public async Task<IEnumerable<MPMarcas>> GetMarcasToUploadAsync(CancellationToken cancellation)
        {
            var MarcasAPI = await MPMarcasReaderService.GetMarcasAPIAsync(cancellation);
            var MarcasERP = await MPMarcasReaderService.GetMarcasERPAsync(cancellation);

            var MarcasRetorno = new List<MPMarcas>();
            foreach (var item in MarcasERP)
            {
                var OldMarca = MarcasAPI.Where(x => x.CODMARCA == item.CODMARCA);

                if (OldMarca.Any())
                {
                    var Marca = OldMarca.First();

                    if (Marca.IDAPI == null || Marca.IDAPIPARTNER == null ||
                        Marca.DESCRICAO.ToUpper() != item.DESCRICAO.ToUpper())
                    {
                        MarcasRetorno.Add(new MPMarcas
                        {
                            CODCONTROLE = Marca.CODCONTROLE,
                            CODMARCA = Marca.CODMARCA,
                            DATASINC = DateTime.Now,
                            STATUSSINC = 0,
                            DESCRICAO = item.DESCRICAO.ToLower().CapitalizeFirst(),
                            IDAPI = Marca.IDAPI,
                            IDAPIPARTNER = Marca.IDAPIPARTNER
                        });
                    }
                }
                else if (!OldMarca.Any())
                {
                    MarcasRetorno.Add(new MPMarcas
                    {
                        CODCONTROLE = 0,
                        CODMARCA = Convert.ToInt32(item.CODMARCA),
                        DATASINC = DateTime.Now,
                        STATUSSINC = 0,
                        DESCRICAO = item.DESCRICAO.ToLower().CapitalizeFirst(),
                        IDAPI = null,
                        IDAPIPARTNER = null
                    });
                }
            }
            return MarcasRetorno;
        }

        public async Task<bool> GenerateMarcasAsync(CancellationToken cancellation, List<MPMarcas> pMarcas)
        {
            try
            {
                MPMarcasRepository.UpdateMarcas(pMarcas.Where(x => x.CODCONTROLE > 0).ToList());
                MPMarcasRepository.AddMarcas(pMarcas.Where(x => x.CODCONTROLE == 0).ToList());

                var rows = await MPMarcasRepository.SaveChangesAsync(cancellation);
                return rows > 0;
            }
            catch (Exception ex)
            {
                var msg = "Não foi possível gerar a lista de marcas do erp";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return false;
            }
        }

        public void Dispose()
        {
            MPMarcasRepository?.Dispose();
        }
    }
}
