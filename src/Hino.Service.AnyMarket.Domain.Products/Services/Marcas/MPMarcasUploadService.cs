using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Marcas;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Marcas
{
    public class MPMarcasUploadService : IMPMarcasUploadService
    {
        public List<string> Errors { get; set; }

        readonly IMPMarcasRepository MPMarcasRepository;

        public MPMarcasUploadService(IMPMarcasRepository pRepository)
        {
            Errors = new List<string>();
            MPMarcasRepository = pRepository;
        }

        public async Task<IEnumerable<MPMarcas>> GetMarcasToUploadAsync(CancellationToken cancellation)
        {
            try
            {
                return await MPMarcasRepository.QueryAsync(cancellation, r => r.STATUSSINC == 0);
            }
            catch (Exception ex)
            {
                var msg = $"Não foi possível buscar as marcas com o status pendente";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return null;
            }
        }

        public void Dispose()
        {
            MPMarcasRepository?.Dispose();
        }
    }
}
