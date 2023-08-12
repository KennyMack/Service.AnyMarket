using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Utils.Request;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class VariationsValue : BaseRest
    {
        public string BASEURN { get; }
        public VariationsValue(
            IHttpClient HttpClient,
            Parameter[] ParamToken)
            : base("values", "(Variações Valores) Variations values",
                  HttpClient,
                  ParamToken)
        {
            BASEURN = "variations";
        }

        public async Task<ResponseBaseDTO<VariationValueDTO>> GetAsync(CancellationToken cancellation, string variationId)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{variationId}/{URN}", Method.GET);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync<VariationValueDTO>(Request, cancellation);

            VariationValueDTO[]? dto = null;
            if (Response.IsSuccessful)
                dto = (JsonSerializer.Deserialize<List<VariationValueDTO>>(Response.Content, options)).ToArray();

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<VariationValueDTO>> GetByIdAsync(CancellationToken cancellation, string variationId, string valueId)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{variationId}/{URN}/{valueId}", Method.GET);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync<VariationValueDTO>(Request, cancellation);

            VariationValueDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<VariationValueDTO>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<VariationValueDTO>> PostAsync(CancellationToken cancellation, string variationId, VariationValueDTO data)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{variationId}/{URN}", Method.POST);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            Request.AddJsonBody(data);

            var Response = await Client.ExecuteAsync<VariationValueDTO>(Request, cancellation);

            VariationValueDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<VariationValueDTO>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<VariationValueDTO>> PutAsync(CancellationToken cancellation, string variationId, string valueId, VariationValueDTO data)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{variationId}/{URN}/{valueId}", Method.PUT);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            Request.AddJsonBody(data);

            var Response = await Client.ExecuteAsync<VariationValueDTO>(Request, cancellation);

            VariationValueDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<VariationValueDTO>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

        public async Task<ResponseBaseDTO<VariationValueDTO>> DeleteAsync(CancellationToken cancellation, string variationId, string valueId)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{variationId}/{URN}/{valueId}", Method.DELETE);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync<VariationValueDTO>(Request, cancellation);

            VariationValueDTO?[]? dto = null;

            return CreateResponse(Response, dto);
        }
    }
}
