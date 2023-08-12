using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Categorias;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Marcas;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Categorias
{
    public class MPCategoriasUploadService : IMPCategoriasUploadService
    {
        public List<string> Errors { get; set; }

        readonly IMPCategoriasRepository MPCategoriasRepository;

        public MPCategoriasUploadService(IMPCategoriasRepository pRepository)
        {
            Errors = new List<string>();
            MPCategoriasRepository = pRepository;
        }

        public async Task<IEnumerable<MPCategorias>> GetCategoriasToUploadAsync(CancellationToken cancellation)
        {
            try
            {
                return await MPCategoriasRepository.QueryAsync(cancellation, r => r.STATUSSINC == 0);
            }
            catch (Exception ex)
            {
                var msg = $"Não foi possível buscar as categorias com o status pendente";
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
