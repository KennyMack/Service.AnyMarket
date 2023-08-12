using NetSwissTools.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    public class ProductSkuDTOConverter : JsonConverter<ProductSkuDTO>
    {
        public override ProductSkuDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ProductSkuDTO sku, JsonSerializerOptions options)
        {
            var usCulture = new CultureInfo("en-US");
            usCulture.NumberFormat.NumberDecimalSeparator = ".";

            var sb = new StringBuilder();
            sb.Append('{');
            sb.Append($"\"title\":\"{sku.title}\",");
            sb.Append($"\"partnerId\":\"{sku.partnerId}\",");
            sb.Append($"\"ean\":\"{sku.ean}\",");
            sb.Append($"\"amount\":{sku.amount.ToString(usCulture)},");
            sb.Append($"\"price\":{sku.price.ToString(usCulture)},");
            sb.Append($"\"sellPrice\":{sku.sellPrice.ToString(usCulture)}");

            if (sku.variations != null)
            {
                sb.Append(',');
                sb.Append("\"variations\":{");

                sb.Append(string.Join(",", sku.variations.Select(variation =>
                {
                    return $"\"{variation.type.name}\":\"{variation.description}\"";
                })));
                sb.Append('}');
            }

            sb.Append('}');

            writer.WriteRawValue(sb.ToString());
        }
    }
}
