using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Categorias;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Marcas;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Categorias
{
    public class MPCategoriasReaderService : IMPCategoriasReaderService
    {
        public List<string> Errors { get; set; }

        readonly IMPCategoriasRepository MPCategoriasRepository;

        public MPCategoriasReaderService(IMPCategoriasRepository pRepository)
        {
            MPCategoriasRepository = pRepository;
        }

        public async Task<IEnumerable<MPCategorias>> GetCategoriasAPIAsync(CancellationToken cancellation)
        {
            try
            {
                return await MPCategoriasRepository.QueryAsync(cancellation, false, exp => exp.CODCONTROLE > 0);
            }
            catch (Exception ex)
            {
                var msg = $"Falha ao buscar as categorias da API";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return null;
            }
        }

        public async Task<IEnumerable<FSFamiliaNiveis>> GetCategoriasERPAsync(CancellationToken cancellation)
        {
            try
            {
                return await MPCategoriasRepository.GetFamiliasERPAsync(cancellation);
            }
            catch (Exception ex)
            {
                var msg = $"Falha ao buscar as categorias do ERP";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return null;
            }
        }

        public void Dispose()
        {
            MPCategoriasRepository?.Dispose();
        }
    }
}
