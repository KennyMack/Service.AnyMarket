using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    public class ImageDTO : BaseResourceDTO
    {
        public int index { get; set; }
        public bool main { get; set; }
        public string url { get; set; }
        public string variation { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this);

        public static ImageDTO FromEntity(ProdImageDTO image)
        {
            return new ImageDTO
            {
                index = image.index,
                main = image.main,
                url = image.url,
                variation = image.variation
            };
        }
    }
}
