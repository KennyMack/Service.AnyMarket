using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Marcas;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Marcas
{
    public class MPMarcasReaderService : IMPMarcasReaderService
    {
        public List<string> Errors { get; set; }

        readonly IMPMarcasRepository MPMarcasRepository;

        public MPMarcasReaderService(IMPMarcasRepository pRepository)
        {
            MPMarcasRepository = pRepository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<MPMarcas>> GetMarcasAPIAsync(CancellationToken cancellation)
        {
            try
            {
                return await MPMarcasRepository.QueryAsync(cancellation, false, exp => exp.CODCONTROLE > 0);
            }
            catch (Exception ex)
            {
                var msg = $"Falha ao buscar as marcas da API";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return null;
            }
        }

        public async Task<IEnumerable<FSMarca>> GetMarcasERPAsync(CancellationToken cancellation)
        {
            try
            {
                return await MPMarcasRepository.GetMarcasERPAsync(cancellation);
            }
            catch (Exception ex)
            {
                var msg = $"Falha ao buscar as marcas do ERP";
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
