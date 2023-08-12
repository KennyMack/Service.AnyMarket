using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;
using NetSwissTools.System;
using NetSwissTools.Utils;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    public class RetVariationTypeDTO : BaseResourceDTO
    {
        public RetVariationTypeDTO()
        {
            values = new();
        }

        public string name { get; set; }
        public bool visualVariation { get; set; }
        public string partnerId { get; set; }

        public List<VariationValueDTO> values { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this);
    }

    public class VariationTypeDTO: BaseResourceDTO
    {
        public VariationTypeDTO()
        {
            values = new();
        }

        public string name { get; set; }
        public string visualVariation { get; set; }
        public string partnerId { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this);

        public List<VariationValueDTO> values { get; set; }

        public static MPTiposVariacao ToEntity(VariationTypeDTO variationType)
        {
            return new MPTiposVariacao
            {
                CODCONTROLE = ConvertEx.ToInt64(variationType.partnerId) ?? 0,
                NOME = variationType.name,
                IDAPI = variationType.id,
                VARIACAOVISUAL = variationType.visualVariation.ToUpper().Trim() == "TRUE",
                Valores = variationType.values?.Select(value => new MPTiposVarValores
                { 
                    CODCONTROLE = ConvertEx.ToInt64(value.partnerId) ?? 0,
                    CODCTRLDET = ConvertEx.ToInt64(variationType.partnerId) ?? 0,
                    DESCRICAO = value.description,
                    IDAPI = value.id
                }).ToArray()

            };
        }

        public static VariationTypeDTO FromEntity(MPTiposVariacao tipoVariacao)
        {
            return new VariationTypeDTO
            {
                id = tipoVariacao.IDAPI ?? 0,
                name = tipoVariacao.NOME.ToLower().CapitalizeFirst(),
                partnerId = tipoVariacao.CODCONTROLE.ToString(),
                visualVariation = tipoVariacao.VARIACAOVISUAL ? "true" : "false",
                values = tipoVariacao.Valores?.Select(valor => new VariationValueDTO
                {
                    id = valor.IDAPI ?? 0,
                    description = valor.DESCRICAO.ToLower().CapitalizeFirst(),
                    partnerId = valor.CODCONTROLE.ToString()
                }).ToList()
            };
        }
    }
}
