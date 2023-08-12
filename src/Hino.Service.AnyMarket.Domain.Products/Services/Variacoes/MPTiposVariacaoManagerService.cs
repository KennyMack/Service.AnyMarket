using Hino.Service.AnyMarket.Domain.Products.Interfaces.Repositories.Marketplace;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Variacoes;
using Hino.Service.AnyMarket.Entities.Marketplace;

namespace Hino.Service.AnyMarket.Domain.Products.Services.Variacoes
{
    public class MPTiposVariacaoManagerService : IMPTiposVariacaoManagerService
    {
        public List<string> Errors { get; set; }

        readonly IMPTiposVarValoresRepository MPTiposVarValoresRepository;
        readonly IMPTiposVariacaoRepository MPTiposVariacaoRepository;

        public MPTiposVariacaoManagerService(
            IMPTiposVarValoresRepository pRepository,
            IMPTiposVariacaoRepository TiposVariacaoRepository)
        {
            Errors = new List<string>();
            MPTiposVariacaoRepository = TiposVariacaoRepository;
            MPTiposVarValoresRepository = pRepository;
        }

        public async Task<IEnumerable<MPTiposVariacao>> GetVariacoesToUploadAsync(CancellationToken cancellation) =>
            await MPTiposVariacaoRepository.QueryAsync(cancellation, r => r.CODCONTROLE > 0, x => x.Valores);

        public async Task UpdateERPAsync(CancellationToken cancellation, List<MPTiposVariacao> pTiposVariacao)
        {
            foreach (var item in pTiposVariacao)
                MPTiposVariacaoRepository.Update(item);

            await MPTiposVariacaoRepository.SaveChangesAsync(cancellation);
        }

        public void Dispose()
        {
            MPTiposVarValoresRepository?.Dispose();
            MPTiposVariacaoRepository?.Dispose();
        }
    }
}
