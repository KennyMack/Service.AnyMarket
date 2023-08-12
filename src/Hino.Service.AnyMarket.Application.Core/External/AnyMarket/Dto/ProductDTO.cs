using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;
using NetSwissTools.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    public class ProductDTO: BaseResourceDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public BrandDTO brand { get; set; }
        public ProdOriginDTO origin { get; set; }
        public CategoryDTO category { get; set; }
        public string model { get; set; }
        public string gender { get; set; }
        public int warrantyTime { get; set; }
        public string warrantyText { get; set; }
        public double height { get; set; }
        public double width { get; set; }
        public double weight { get; set; }
        public double length { get; set; }
        public double priceFactor { get; set; }
        public bool calculatedPrice { get; set; }

        public ProductSkuDTO[] skus { get; set; }
        public ProdCharacteristicDTO[] characteristics { get; set; }
        public ProdImageDTO[] images { get; set; }

        public static string ConvertGender(short genero) =>
            genero switch
            {
                1 => "MALE",
                2 => "FEMALE",
                3 => "BOY",
                4 => "GIRL",
                5 => "UNISSEX",
                _ => null,
            };

        public static ProductDTO FromEntity(MPProdutos produto, IEnumerable<MPProdutos> variations)
        {
            var product = new ProductDTO
            {
                id = produto.IDAPI ?? 0,
                title = produto.TITULO,
                description = produto.DESCRICAO,
                priceFactor = 1,
                calculatedPrice = false,
                model = produto.MODELO,
                gender = ConvertGender(produto.GENERO),
                warrantyTime = produto.TEMPOGARANTIA ?? 0,
                warrantyText = produto.AVISOGARANTIA
            };

            var sku = new ProductSkuDTO
            {
                id = produto.IDAPISKU,
                title = product.title,
                partnerId = produto.CODPRODUTO,
                ean = produto.CODBARRAS,
            };

            if (produto.ProdDetalhes != null)
            {
                product.weight = produto.ProdDetalhes.PESOBRUTO;
                product.height = produto.ProdDetalhes.ALTURA;
                product.width = produto.ProdDetalhes.LARGURA;
                product.length = produto.ProdDetalhes.COMPRIMENTO;

                sku.amount = produto.ProdDetalhes.SALDOESTOQUE;

                if (produto.ProdDetalhes.CODORIGMERC != null)
                {
                    product.origin = new ProdOriginDTO
                    {
                        id = ConvertEx.ToInt32(produto.ProdDetalhes.CODORIGMERC) ?? 0
                    };
                }
                if (produto.ProdDetalhes.CODMARCA != null)
                {
                    product.brand = new BrandDTO
                    {
                        id = ConvertEx.ToInt32(produto.ProdDetalhes.CODMARCA) ?? 0,
                        name = produto.ProdDetalhes.DESCRICAO
                    };
                }
                if (produto.ProdDetalhes.CATEGORIA != null)
                {
                    product.category = new CategoryDTO
                    {
                        id = ConvertEx.ToInt32(produto.ProdDetalhes.CATEGORIA) ?? 0
                    };
                }
            }

            if (produto.ProdAtributos != null &&
                (produto.ProdAtributos?.Any() ?? false))
            {
                var atribList = produto.ProdAtributos.ToArray();
                var length = produto.ProdAtributos.Count;
                product.characteristics = new ProdCharacteristicDTO[length];
                for (int i = 0; i < length; i++)
                {
                    product.characteristics[i] = new ProdCharacteristicDTO
                    {
                        index = i,
                        name = atribList[i].CARACTERISTICA,
                        value = atribList[i].VALOR
                    };
                }
            }
            if (produto.ProdImagens != null &&
                (produto.ProdImagens?.Any() ?? false))
            {
                var imgList = produto.ProdImagens.ToArray();
                var length = produto.ProdImagens.Count;
                product.images = new ProdImageDTO[length];
                for (int i = 0; i < length; i++)
                {
                    product.images[i] = new ProdImageDTO
                    {
                        index = ConvertEx.ToInt32(imgList[i].INDICE) ?? 0,
                        url = imgList[i].URL,
                        main = imgList[i].PRINCIPAL == 1,
                        variation = imgList[i].ProdVariacaoVlr?.DESCRICAO
                    };
                }
            }

            if (produto.ProdPreco != null &&
                (produto.ProdPreco?.Any() ?? false))
            {
                sku.price = Convert.ToDouble(produto.ProdPreco.First().VALORUNITARIO);
                sku.sellPrice = sku.price;
            }


            if (variations.Count() == 1)
            {
                product.skus = new ProductSkuDTO[]
                {
                    sku
                };
            }
            else
            {
                product.skus = variations.Select(x =>
                {
                    return new ProductSkuDTO
                    {
                        title = x.TITULO,
                        partnerId = x.CODPRODUTO,
                        ean = x.CODBARRAS,
                        price = sku.price,
                        sellPrice = sku.sellPrice,
                        amount = x.ProdDetalhes?.SALDOESTOQUE ?? 0,
                        id = x.IDAPISKU,
                        variations = new VariationValueDTO[]
                        {
                            VariationValueDTO.FromEntity(
                                x.ProdVariacaoVlr
                            )
                        }
                    };
                }).ToArray();
            }

            return product;
        }

        public override string ToString() =>
            JsonSerializer.Serialize(this);
    }

    public class ProdImageDTO : BaseResourceDTO
    {
        public int index { get; set; }
        public bool main { get; set; }
        public string url { get; set; }
        public string variation { get; set; }
    }

    public class ProdCharacteristicDTO: BaseResourceDTO
    {
        public long index { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

    public class ProdOriginDTO : BaseResourceDTO
    {

    }

}
