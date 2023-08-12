using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
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
    public class MPProductsReaderService : IMPProductsReaderService
    {
        public List<string> Errors { get; set; }

        readonly IMPProdutosRepository MPProdutosRepository;

        public MPProductsReaderService(IMPProdutosRepository pRepository)
        {
            Errors = new List<string>();
            MPProdutosRepository = pRepository;
        }

        public async Task<IEnumerable<MPProdutos>> GetProductsAPIAsync(CancellationToken cancellation)
        {
            try
            {
                var list = await MPProdutosRepository.GetProductsAPIAsync(cancellation);

                /*var list = await MPProdutosRepository.QueryAsync(cancellation, false,
                    exp => exp.STATUSSINC == 0 && exp.LIBERADOSINC == 1 &&
                           exp.CODGRUPOVARIACAO == "110.00974",
                    x => x.ProdVariacaoVlr,
                    x => x.ProdVariacaoVlr.Tipo,
                    x => x.ProdAtributos,
                    x => x.ProdImagens,
                    x => "ProdImagens",
                    x => x.ProdPreco);*/

                foreach (var product in list)
                {
                    product.ProdImagens = product.ProdImagens.Where(x => !x.EXCLUIDO).ToList();

                    product.ProdDetalhes =
                        await MPProdutosRepository.GetProductDetailsAsync(cancellation, 
                            product.CODESTAB, product.CODPRODUTO);
                }

                return list;
            }
            catch (Exception ex)
            {
                var msg = $"Falha ao buscar os produtos da API";
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
