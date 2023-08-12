using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Utils.Request;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class Skus : BaseRest
    {
        public string BASEURN { get; }
        public Skus(
            IHttpClient HttpClient,
            Parameter[] ParamToken)
            : base("skus", "(Skus) Skus",
                  HttpClient,
                  ParamToken)
        {
            BASEURN = "products";
        }


        public async Task<ResponseBaseDTO<ProductSkuDTO>> GetAsync(CancellationToken cancellation, string productId)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{productId}/{URN}", Method.GET);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync(Request, cancellation);

            ProductSkuDTO[]? dto = null;
            if (Response.IsSuccessful)
                dto = (JsonSerializer.Deserialize<List<ProductSkuDTO>>(Response.Content, options)).ToArray();

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<ProductSkuDTO>> GetByIdAsync(CancellationToken cancellation, string productId, string skuId)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{productId}/{URN}/{skuId}", Method.GET);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync(Request, cancellation);

            ProductSkuDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<ProductSkuDTO>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<ProductSkuDTO>> PostAsync(CancellationToken cancellation, string productId, ProductSkuDTO data)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{productId}/{URN}", Method.POST);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            JsonSerializerOptions options = new();
            options.Converters.Add(new ProductSkuDTOConverter());

            string json = JsonSerializer.Serialize(data, options);

            Request.AddJsonBody(json);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            ProductSkuDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<ProductSkuDTO>(Response.Content) };

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<ProductSkuDTO>> PutAsync(CancellationToken cancellation, string productId, string skuId, ProductSkuDTO data)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{productId}/{URN}/{skuId}", Method.PUT);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            JsonSerializerOptions options = new();
            options.Converters.Add(new ProductSkuDTOConverter());

            string json = JsonSerializer.Serialize(data, options);

            Request.AddJsonBody(json);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            ProductSkuDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<ProductSkuDTO>(Response.Content) };

            return CreateResponse(Response, dto);
        }

        public async Task<ResponseBaseDTO<ProductSkuDTO>> DeleteAsync(CancellationToken cancellation, string productId, string skuId)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{productId}/{URN}/{skuId}", Method.DELETE);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync(Request, cancellation);

            ProductSkuDTO?[]? dto = null;

            return CreateResponse(Response, dto);
        }
    }
}
