using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;
using NetSwissTools.System;
using NetSwissTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    public class StockLocalDTO : BaseResourceDTO
    {
        public string name { get; set; }
        [JsonPropertyName("virtual")]
        public bool Virtual { get; set; }
        public bool defaultLocal { get; set; }
        public string zipCode { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this);

        public static StockLocalDTO FromEntity(MPEstoque estoque)
        {
            return new StockLocalDTO
            {
                id = estoque.IDAPI,
                name = estoque.DESCRICAO.ToLower().CapitalizeFirst(),
                Virtual = estoque.VIRTUAL,
                defaultLocal = estoque.PADRAO,
                zipCode = null
            };
        }
    }
}
