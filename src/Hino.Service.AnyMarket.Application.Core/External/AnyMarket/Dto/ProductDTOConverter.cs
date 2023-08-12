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
    public class ProductDTOConverter : JsonConverter<ProductDTO>
    {
        public override ProductDTO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ProductDTO product, JsonSerializerOptions options)
        {
            var usCulture = new CultureInfo("en-US");
            usCulture.NumberFormat.NumberDecimalSeparator = ".";

            var sb = new StringBuilder();
            sb.Append('{');
            sb.Append($"\"title\":\"{product.title}\",");
            sb.Append($"\"description\":\"{product.description}\",");
            sb.Append($"\"model\":\"{product.model}\",");
            if (!product.gender.IsEmpty())
                sb.Append($"\"gender\":\"{product.gender}\",");
            
            if (product.warrantyTime > 0)
                sb.Append($"\"warrantyTime\":\"{product.warrantyTime}\",");
            
            if (!product.warrantyText.IsEmpty())
                sb.Append($"\"warrantyText\":\"{product.warrantyText}\",");
            
            if (product.height > 0)
                sb.Append($"\"height\":{product.height.ToString(usCulture)},");
            
            if (product.width > 0)
                sb.Append($"\"width\":{product.width.ToString(usCulture)},");
            
            if (product.weight > 0)
                sb.Append($"\"weight\":{product.weight.ToString(usCulture)},");
            
            if (product.length > 0)
                sb.Append($"\"length\":{product.length.ToString(usCulture)},");
            
            if (product.priceFactor > 0)
                sb.Append($"\"priceFactor\":{product.priceFactor.ToString(usCulture)},");

            var strCalculatedPrice = product.calculatedPrice ? "true" : "false";
            sb.Append($"\"calculatedPrice\":{strCalculatedPrice},");
            
            sb.Append("\"brand\":{");
            sb.Append($"\"name\":\"{product.brand.name}\",");
            sb.Append($"\"id\":{product.brand.id}");
            sb.Append("},");
            
            sb.Append("\"origin\":{");
            sb.Append($"\"id\":{product.origin.id}");
            sb.Append("},");
            
            sb.Append("\"category\":{");
            sb.Append($"\"id\":{product.category.id}");
            sb.Append("},");

            sb.Append("\"skus\":[");
            var lstSkus = string.Join(",", product.skus.Select(sku =>
            {
                var sbsku = new StringBuilder();
                sbsku.Append('{');
                sbsku.Append($"\"title\":\"{sku.title}\",");
                sbsku.Append($"\"partnerId\":\"{sku.partnerId}\",");
                sbsku.Append($"\"ean\":\"{sku.ean}\",");
                sbsku.Append($"\"amount\":{sku.amount.ToString(usCulture)},");
                sbsku.Append($"\"price\":{sku.price.ToString(usCulture)},");
                sbsku.Append($"\"sellPrice\":{sku.sellPrice.ToString(usCulture)}");

                if (sku.variations != null)
                {
                    sbsku.Append(',');
                    sbsku.Append("\"variations\":{");

                    sbsku.Append(string.Join(",", sku.variations.Select(variation =>
                    {
                        return $"\"{variation.type.name}\":\"{variation.description}\"";
                    })));
                    sbsku.Append('}');
                }

                sbsku.Append('}');
                return sbsku.ToString();
            }));
            sb.Append(lstSkus);
            sb.Append("],");

            sb.Append("\"characteristics\":[");

            var lstCharacteristics = string.Join(",", product.characteristics.Select(x =>
            {
                var sbchar = new StringBuilder();

                sbchar.Append('{');
                sbchar.Append($"\"index\":{x.index},");
                sbchar.Append($"\"name\":\"{x.name}\",");
                sbchar.Append($"\"value\":\"{x.value}\"");
                sbchar.Append('}');

                return sbchar.ToString();
            }));

            sb.Append(lstCharacteristics);
            sb.Append("],");

            /*
            sb.Append("\"images\":[");
            var lstImages = string.Join(",", product.images.Select(image =>
            {
                var sbimg = new StringBuilder();
                sbimg.Append('{');
                var strMainImage = image.main ? "true" : "false";
                sbimg.Append($"\"index\":{image.index},");
                sbimg.Append($"\"main\":{strMainImage},");
                sbimg.Append($"\"url\":\"{image.url}\",");

                var strIdValue = image.id != null ? image.id.ToString() : "null";

                sbimg.Append($"\"id\":{strIdValue}");

                if (!image.variation.IsEmpty())
                {
                    sbimg.Append(',');
                    sbimg.Append($"\"variation\":\"{image.variation}\"");
                }

                sbimg.Append('}');

                return sbimg.ToString();
            }));
            sb.Append(lstImages);
            sb.Append("],");
            */

            var strId = product.id != null ? product.id.ToString() : "null";
            sb.Append($"\"id\":{strId}");

            sb.Append('}');

            writer.WriteRawValue(sb.ToString());
        }
    }
}
