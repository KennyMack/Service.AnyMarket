using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Logs;
using Hino.Service.AnyMarket.Utils.Request;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class Images : BaseRest
    {
        public string BASEURN { get; }
        public Images(
            IHttpClient HttpClient,
            Parameter[] ParamToken)
            : base("images", "(Imagens) images",
                  HttpClient,
                  ParamToken)
        {
            BASEURN = "products";
        }

        public async Task<ResponseBaseDTO<ImageDTO>> GetAsync(CancellationToken cancellation, string productId)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{productId}/{URN}", Method.GET);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync(Request, cancellation);

            ImageDTO[]? dto = null;
            if (Response.IsSuccessful)
                dto = (JsonSerializer.Deserialize<List<ImageDTO>>(Response.Content, options)).ToArray();

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<ImageDTO>> GetByIdAsync(CancellationToken cancellation, string productId, string imageId)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{productId}/{URN}/{imageId}", Method.GET);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync(Request, cancellation);

            ImageDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<ImageDTO>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<ImageDTO>> PostAsync(CancellationToken cancellation, string productId, ImageDTO data)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{productId}/{URN}", Method.POST);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            Request.AddJsonBody(data);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            ImageDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<ImageDTO>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<ImageDTO>> PutAsync(CancellationToken cancellation, string productId, string imageId, ImageDTO data)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{productId}/{URN}/{imageId}", Method.PUT);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            Request.AddJsonBody(data);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            ImageDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<ImageDTO>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

        public async Task<ResponseBaseDTO<ImageDTO>> DeleteAsync(CancellationToken cancellation, string productId, string imageId)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{BASEURN}/{productId}/{URN}/{imageId}", Method.DELETE);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync(Request, cancellation);

            ImageDTO?[]? dto = null;

            return CreateResponse(Response, dto);
        }
    }
}
