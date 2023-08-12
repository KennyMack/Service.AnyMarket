using NetSwissTools.System; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    internal class StockBalanceDTOConverter : JsonConverter<StockBalanceDTO>
    {

        public override StockBalanceDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            var stockBalanceReturn = JsonSerializer.Deserialize<StockBalanceReturnDTO>(ref reader);
            return stockBalanceReturn.ToStockBalanceDTO();
        }

        public override void Write(Utf8JsonWriter writer, StockBalanceDTO sku, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
