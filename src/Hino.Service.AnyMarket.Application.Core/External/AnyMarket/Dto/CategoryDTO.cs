using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Entities.Marketplace;
using Hino.Service.AnyMarket.Utils;
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
    public class CategoryDTO: BaseResourceDTO
    {
        public string name { get; set; }
        public string partnerId { get; set; }
        public decimal priceFactor { get; set; }
        public string definitionPriceScope { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this);

        public CategoryParentDTO parent { get; set; }

        public static MPCategorias ToEntity(CategoryDTO[] category)
        {
            var RetObj = new MPCategorias();

            for (int i = 0, length = category.Length; i < length; i++)
            {
                switch (i)
                {
                    case 0:
                        RetObj.CODFAMILIA = category[i].partnerId;
                        RetObj.DESCFAMILIA = category[i].name;
                        RetObj.IDCATEGNV1 = category[i].id == 0 ? null : category[i].id;
                        break;
                    case 1:
                        RetObj.CODFAMILIA = category[i].partnerId;
                        RetObj.DESCFAMILIA = category[i].name;
                        RetObj.IDCATEGNV2 = category[i].id == 0 ? null : category[i].id;
                        break;
                    case 2:
                        RetObj.CODFAMILIA = category[i].partnerId;
                        RetObj.DESCFAMILIA = category[i].name;
                        RetObj.IDCATEGNV3 = category[i].id == 0 ? null : category[i].id;
                        break;
                    case 3:
                        RetObj.CODFAMILIA = category[i].partnerId;
                        RetObj.DESCFAMILIA = category[i].name;
                        RetObj.IDCATEGNV4 = category[i].id == 0 ? null : category[i].id;
                        break;
                }
            }

            return RetObj;
        }

        public static CategoryTreeDTO GenerateTree(MPCategorias[] categories)
        {
            var categoriesRet = categories.Select(category => new CategoryDTO
            {
                id = category.IDCATEGNV4,
                partnerId = $"CAT_{category.CODCATEGORIA}",
                name = (category.DESCCATEGORIA ?? "").ToLower()
                    .ClearSpecialChars().CapitalizeFirst(),
                priceFactor = 1,
                definitionPriceScope = "SKU"
            }).Distinct(new CategoryDTOEqualityComparer());

            var classeRet = categories.Select(category => new CategoryDTO
            {
                id = category.IDCATEGNV3,
                partnerId = $"CLA_{category.CODCLASSE}",
                name = (category.DESCCLASSE ?? "").ToLower()
                    .ClearSpecialChars().CapitalizeFirst(),
                priceFactor = 1,
                definitionPriceScope = "SKU",
                parent = new CategoryParentDTO
                {
                    id = category.IDCATEGNV4 ?? 0,
                    parentId = $"CAT_{category.CODCATEGORIA}"
                }
            }).Distinct(new CategoryDTOEqualityComparer());

            var grupoRet = categories.Select(category => new CategoryDTO
            {
                id = category.IDCATEGNV2,
                partnerId = $"GRU_{category.CODGRUPO}",
                name = (category.DESCGRUPO?? "").ToLower()
                    .ClearSpecialChars().CapitalizeFirst(),
                priceFactor = 1,
                definitionPriceScope = "SKU",
                parent = new CategoryParentDTO
                {
                    id = category.IDCATEGNV3 ?? 0,
                    parentId = $"CLA_{category.CODCLASSE}"
                }
            }).Distinct(new CategoryDTOEqualityComparer());

            var familiaRet = categories.Select(category => new CategoryDTO
            {
                id = category.IDCATEGNV1,
                partnerId = $"FAM_{category.CODFAMILIA}",
                name = (category.DESCFAMILIA ?? "").ToLower()
                    .ClearSpecialChars().CapitalizeFirst(),
                priceFactor = 1,
                definitionPriceScope = "SKU",
                parent = new CategoryParentDTO
                {
                    id = category.IDCATEGNV2 ?? 0,
                    parentId = $"GRU_{category.CODGRUPO}"
                }
            }).Distinct(new CategoryDTOEqualityComparer());

            return new CategoryTreeDTO
            {
                Categorias = categoriesRet.ToArray(),
                Classes = classeRet.ToArray(),
                Grupos = grupoRet.ToArray(),
                Familias = familiaRet.ToArray()
            };
        }

        public static CategoryDTO[] FromEntity(MPCategorias category)
        {
            var lst = new List<CategoryDTO>();

            if (!category.CODFAMILIA.IsEmpty())
                lst.Add(new CategoryDTO
                {
                    id = category.IDCATEGNV1 ?? 0,
                    name = (category.DESCFAMILIA ?? "").ToLower()
                        .ClearSpecialCharacteres().CapitalizeFirst(),
                    partnerId = $"FAM_{category.CODFAMILIA}",
                    priceFactor = 1,
                    definitionPriceScope = "SKU"
                });
            if (!category.CODGRUPO.IsEmpty())
                lst.Add(new CategoryDTO
                {
                    id = category.IDCATEGNV2 ?? 0,
                    name = (category.DESCGRUPO ?? "").ToLower()
                        .ClearSpecialCharacteres().CapitalizeFirst(),
                    partnerId = $"GRU_{category.CODGRUPO}",
                    priceFactor = 1,
                    definitionPriceScope = "SKU"
                });
            if (!category.CODCLASSE.IsEmpty())
                lst.Add(new CategoryDTO
                {
                    id = category.IDCATEGNV3 ?? 0,
                    name = (category.DESCCLASSE ?? "").ToLower()
                        .ClearSpecialCharacteres().CapitalizeFirst(),
                    partnerId = $"CLA_{category.CODCLASSE}",
                    priceFactor = 1,
                    definitionPriceScope = "SKU"
                });
            if (!category.CODCATEGORIA.IsEmpty())
                lst.Add(new CategoryDTO
                {
                    id = category.IDCATEGNV4 ?? 0,
                    name = (category.DESCCATEGORIA ?? "").ToLower()
                        .ClearSpecialCharacteres().CapitalizeFirst(),
                    partnerId = $"CAT_{category.CODCATEGORIA}",
                    priceFactor = 1,
                    definitionPriceScope = "SKU"
                });

            return lst.ToArray();
        }
    }

    public class CategoryParentDTO
    {
        public long id { get; set; }

        [JsonIgnore]
        public string parentId { get; set; }
    }

    public class CategoryTreeDTO
    {
        public CategoryDTO[] Categorias { get; set; }
        public CategoryDTO[] Classes { get; set; }
        public CategoryDTO[] Grupos { get; set; }
        public CategoryDTO[] Familias { get; set; }

    }

    class CategoryDTOEqualityComparer : IEqualityComparer<CategoryDTO>
    {
        public bool Equals(CategoryDTO x, CategoryDTO y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.name == y.name && 
                x.partnerId == y.partnerId && 
                x.priceFactor == y.priceFactor && 
                x.definitionPriceScope == y.definitionPriceScope;
        }

        public int GetHashCode(CategoryDTO obj)
        {
            unchecked
            {
                int hashCode = obj.name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (obj.partnerId?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ obj.priceFactor.GetHashCode();
                hashCode = (hashCode * 397) ^ (obj.definitionPriceScope?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
