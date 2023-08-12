using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Logs;
using Hino.Service.AnyMarket.Utils.Request;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class Stocks : BaseRest
    {
        public Stocks(
            IHttpClient HttpClient,
            Parameter[] ParamToken)
            : base("stocks", "(Saldos de estoque) stocks",
                  HttpClient,
                  ParamToken)
        {
        }

        public async Task<ResponseBaseListDTO<T>> GetListAsync<T>(CancellationToken cancellation, int limit, int offset) where T : BaseResourceDTO
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}?limit={limit}&offset={offset}", Method.GET);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            JsonSerializerOptions optionsList = new();
            optionsList.Converters.Add(new StockBalanceDTOConverter());

            var Response = await Client.ExecuteAsync(Request, cancellation);

            ResponseBaseListDTO<T>? dto = null;
            if (Response.IsSuccessful)
                dto = JsonSerializer.Deserialize<ResponseBaseListDTO<T>>(Response.Content, optionsList);

            return CreatePagedResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<T>> PostPageAsync<T>(CancellationToken cancellation, T[] data) where T : BaseResourceDTO
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}", Method.POST);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            Request.AddJsonBody(data);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            return new ResponseBaseDTO<T>
            {
                Content = Response?.Content ?? "",
                StatusCode = Response?.StatusCode ?? System.Net.HttpStatusCode.NotAcceptable,
                IsSuccessful = Response?.IsSuccessful ?? false,
                ErrorMessage = Response?.ErrorMessage ?? ""
            };
        }

        public async virtual Task<ResponseBaseDTO<T>> PutPageAsync<T>(CancellationToken cancellation, T[] data) where T : BaseResourceDTO
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}", Method.PUT);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            Request.AddJsonBody(data);

            var Response = await Client.ExecuteAsync(Request, cancellation);

            return new ResponseBaseDTO<T>
            {
                Content = Response?.Content ?? "",
                StatusCode = Response?.StatusCode ?? System.Net.HttpStatusCode.NotAcceptable,
                IsSuccessful = Response?.IsSuccessful ?? false,
                ErrorMessage = Response?.ErrorMessage ?? ""
            };
        }
    }
}
