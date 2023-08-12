using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;
using NetSwissTools.System;
using NetSwissTools.Utils;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    public class BrandDTO: BaseResourceDTO
    {
        public string name { get; set; }
        public string reducedName { get; set; }
        public string partnerId { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this);

        public static MPMarcas ToEntity(BrandDTO brand)
        {
            return new MPMarcas
            {
                CODMARCA = ConvertEx.ToInt32(brand.partnerId ?? "0") ?? 0,
                DESCRICAO = brand.name
            };
        }

        public static BrandDTO FromEntity(MPMarcas marca)
        {
            return new BrandDTO
            {
                id = marca.IDAPI,
                name = marca.DESCRICAO.ToLower().CapitalizeFirst(),
                partnerId = marca.CODMARCA.ToString()
            };
        }
    }
}
