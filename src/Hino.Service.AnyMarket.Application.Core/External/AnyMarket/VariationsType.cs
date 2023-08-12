using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Logs;
using Hino.Service.AnyMarket.Utils.Request;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class VariationsType : BaseRest
    {
        public VariationsType(
            IHttpClient HttpClient,
            Parameter[] ParamToken)
            : base("variations", "(Variações Valores) Variations values",
                  HttpClient,
                  ParamToken)
        {
        }

        public async virtual Task<ResponseBaseDTO<RetVariationTypeDTO>> GetTypeByIdAsync(CancellationToken cancellation, string id)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}/{id}", Method.GET);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync(Request, cancellation);

            RetVariationTypeDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<RetVariationTypeDTO>(Response.Content) };

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<RetVariationTypeDTO>> PostCreateAsync(CancellationToken cancellation, VariationTypeDTO data)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}", Method.POST);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var json = data.ToString();

            Request.AddJsonBody(json);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            RetVariationTypeDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<RetVariationTypeDTO>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<RetVariationTypeDTO>> PutChangeAsync(CancellationToken cancellation, string id, VariationTypeDTO data)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}/{id}", Method.PUT);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            Request.AddJsonBody(data);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            RetVariationTypeDTO?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<RetVariationTypeDTO>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

    }
}
