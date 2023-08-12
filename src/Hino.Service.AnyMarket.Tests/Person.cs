using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Tests
{
    internal class Person
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public Address Address { get; set; }
    }

    internal class Address
    {
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }

    internal class PersonConverter : JsonConverter<Person>
    {
        public override Person? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Person product, JsonSerializerOptions options)
        {
            /*
            writer.WriteRawValue($@"{{""name"": ""{person.Name}""}}");

            var json = $@"{{
  ""title"": ""{product.title}"",
  ""description"": ""{product.description}"",
  ""brand"": {{
    ""name"": ""{product.brand.name}"",
    ""reducedName"": ""{product.brand.reducedName}"",
    ""partnerId"": ""{product.brand.partnerId}"",
    ""id"": {product.brand.id}
  }},
  ""origin"": {{
    ""id"": {product.origin.id}
  }},
  ""category"": {{
    ""id"": {product.category.id}
  }},
  ""model"": ""{product.model}"",
  ""gender"": {product.gender.IsEmpty() ? null : ""{product.gender}""},
  ""warrantyTime"": {product.warrantyTime},
  ""warrantyText"": {product.warrantyText.IsEmpty() ? null : ""{product.warrantyText}""},
  ""height"": {product.height},
  ""width"": {product.width},
  ""weight"": {product.weight},
  ""length"": {product.length},
  ""priceFactor"": {product.priceFactor},
  ""calculatedPrice"": { product.calculatedPrice ? "true" : "false"},
  ""skus"": [
    {{
      ""title"": ""Refletivo Camisa P Rigido"",
      ""partnerId"": ""110.00974"",
      ""ean"": ""11000974"",
      ""amount"": 0,
      ""price"": 4.56599,
      ""sellPrice"": 4.56599,
      ""variations"": {{
        ""VARIACAO TAMANHO"": ""tamanho p""
      }},
      ""id"": null
    }},
    {{
      ""title"": ""Refletivo Camisa P Rigido"",
      ""partnerId"": ""110.00975"",
      ""ean"": ""11000975"",
      ""amount"": 0,
      ""price"": 4.56599,
      ""sellPrice"": 4.56599,
      ""variations"": {{
        ""VARIACAO TAMANHO"": ""tamanho m""
      }},
      ""id"": null
    }},
    {{
      ""title"": ""Refletivo Camisa P Rigido"",
      ""partnerId"": ""110.00980"",
      ""ean"": ""11000980"",
      ""amount"": 0,
      ""price"": 4.56599,
      ""sellPrice"": 4.56599,
      ""variations"": {{
        ""VARIACAO TAMANHO"": ""tamanho g""
      }},
      ""id"": null
    }}
  ],
  ""characteristics"": [
    {{
      ""index"": 0,
      ""name"": ""Material"",
      ""value"": ""dfgdfg"",
      ""id"": null
    }}
  ],
  ""images"": [
    {{
      ""index"": 1,
      ""main"": true,
      ""url"": ""https://quatrorodas.abril.com.br/wp-content/uploads/2023/07/1FLP0573-e1689708954678.jpg"",
      ""variation"": null,
      ""id"": null
    }},
    {{
      ""index"": 2,
      ""main"": false,
      ""url"": ""https://quatrorodas.abril.com.br/wp-content/uploads/2023/07/1FLP0573-e1689708954678.jpg"",
      ""variation"": ""tamanho p"",
      ""id"": null
    }},
    {{
      ""index"": 3,
      ""main"": false,
      ""url"": ""https://quatrorodas.abril.com.br/wp-content/uploads/2023/07/1FLP0573-e1689708954678.jpg"",
      ""variation"": ""tamanho m"",
      ""id"": null
    }},
    {{
      ""index"": 4,
      ""main"": false,
      ""url"": ""https://quatrorodas.abril.com.br/wp-content/uploads/2023/07/1FLP0573-e1689708954678.jpg"",
      ""variation"": ""tamanho g"",
      ""id"": null
    }}
  ],
  ""id"": {product.id}
}}";*/


            /*writer.WriteStartObject();

            writer.WritePropertyName("fullName");
            writer.WriteStringValue($"{person.Name}({person.FirstName})");
            writer.WritePropertyName("id");
            writer.WriteStringValue($"{person.Id}");

            writer.WritePropertyName("address");
            writer.WriteStartObject();
            writer.WritePropertyName(person.Address.City);
            writer.WriteStringValue(person.Address.State);
            writer.WritePropertyName("PostalCode");
            writer.WriteStringValue(person.Address.PostalCode);

            writer.WriteEndObject();

            writer.WriteEndObject();*/
        }
    }

}
