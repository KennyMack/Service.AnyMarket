using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Logs;
using Hino.Service.AnyMarket.Utils.Request;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class StocksLocals : BaseRest
    {
        public StocksLocals(
            IHttpClient HttpClient,
            Parameter[] ParamToken)
            : base("stocks/locals", "(Locais de estoque) stocks/locals",
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

            var json = data.ToString();

            Request.AddJsonBody(json);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            T?[]? dto = null;
            if (Response.IsSuccessful)
            {
                var dtoData = JsonSerializer.Deserialize<ResponseBaseWithMessageDTO<T>>(Response.Content, options);
                
                dto = new[] { dtoData.data };
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

            var json = data.ToString();

            Request.AddJsonBody(json);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            T?[]? dto = null;
            if (Response.IsSuccessful)
            {
                var dtoData = JsonSerializer.Deserialize<ResponseBaseWithMessageDTO<T>>(Response.Content, options);

                dto = new[] { dtoData.data };
            }

            return CreateResponse(Response, dto);
        }
    }
}
