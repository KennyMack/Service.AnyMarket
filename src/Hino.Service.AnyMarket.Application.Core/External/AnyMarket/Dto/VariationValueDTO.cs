using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;
using NetSwissTools.Utils;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    public class VariationValueDTO : BaseResourceDTO
    {
        public string description { get; set; }
        public string partnerId { get; set; }

        public RetVariationTypeDTO type { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this);

        public static VariationValueDTO FromEntity(MPTiposVarValores tipoVarValor)
        {
            return new VariationValueDTO
            {
                id = tipoVarValor.IDAPI ?? 0,
                description = tipoVarValor.DESCRICAO.ToLower().CapitalizeFirst(),
                partnerId = tipoVarValor.CODCONTROLE.ToString(),
                type = new RetVariationTypeDTO
                {
                    id = tipoVarValor.IDAPI ?? 0,
                    partnerId = tipoVarValor.CODCONTROLE.ToString(),
                    name = tipoVarValor.Tipo?.NOME,
                    visualVariation = tipoVarValor.Tipo?.VARIACAOVISUAL ?? false
                }
            };
        }
    }
}
