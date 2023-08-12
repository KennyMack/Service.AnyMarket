using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    public class ProductSkuDTO : BaseResourceDTO
    {
        private string _title;
        public string title
        {
            get
            {
                if (variations?.Length > 0)
                    return $"{_title}-{variations[0].description}";
                return _title;
            }
            set => _title = value;
        }
        public string partnerId { get; set; }
        public string ean { get; set; }
        public double amount { get; set; }
        public double price { get; set; }
        public double sellPrice { get; set; }

        public VariationValueDTO[] variations { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this);
    }
}
