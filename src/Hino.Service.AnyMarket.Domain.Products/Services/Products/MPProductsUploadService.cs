using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Categorias;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Products;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Products
{
    public class MPProductsUploadService : IMPProductsUploadService
    {
        public List<string> Errors { get; set; }

        readonly IMPProdutosRepository MPProdutosRepository;

        public MPProductsUploadService(IMPProdutosRepository pRepository)
        {
            Errors = new List<string>();
            MPProdutosRepository = pRepository;
        }

        public async Task<IEnumerable<MPProdutos>> GetProductsToUploadAsync(CancellationToken cancellation)
        {
            try
            {
                var products = await MPProdutosRepository.QueryAsync(cancellation, false, r => r.STATUSSINC == 0 && r.LIBERADOSINC == 1,
                    x => x.ProdAtributos,
                    x => x.ProdEstoque,
                    x => x.ProdImagens,
                    x => x.ProdPreco);

                return products;
            }
            catch (Exception ex)
            {
                var msg = $"Não foi possível buscar os produtos com o status pendente e liberados";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return null;
            }
        }

        public void Dispose()
        {
            MPProdutosRepository?.Dispose();
        }
    }
}
