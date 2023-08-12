using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Utils.Request;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class Products : BaseRest
    {
        public Products(
            IHttpClient HttpClient,
            Parameter[] ParamToken)
            : base("products", "(Produtos) Products",
                  HttpClient,
                  ParamToken)
        {
        }

        public override async Task<ResponseBaseDTO<T>> PostAsync<T>(CancellationToken cancellation, T data)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}", Method.POST);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            JsonSerializerOptions options = new();
            options.Converters.Add(new ProductDTOConverter());

            string json = JsonSerializer.Serialize(data, options);

            Request.AddJsonBody(json);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            T?[]? dto = null;
            if (Response.IsSuccessful)
            {

                dto = new[] { JsonSerializer.Deserialize<T>(Response.Content) };
            }

            return CreateResponse(Response, dto);
        }

        public override async Task<ResponseBaseDTO<T>> PutAsync<T>(CancellationToken cancellation, string id, T data)
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}/{id}", Method.PUT);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            JsonSerializerOptions options = new();
            options.Converters.Add(new ProductDTOConverter());

            string json = JsonSerializer.Serialize(data, options);

            Request.AddJsonBody(json);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            T?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<T>(Response.Content) };

            return CreateResponse(Response, dto);
        }
    }
}
