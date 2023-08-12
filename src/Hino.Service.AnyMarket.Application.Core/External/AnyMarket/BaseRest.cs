using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Utils.Request;
using NetSwissTools.Utils;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class BaseRest : IBaseRest
    {
        public BaseRest(
            string URN,
            string CurrentResource,
            IHttpClient HttpClient,
            Parameter[] ParamToken)
        {
            this.CurrentResource = CurrentResource;
            this.URN = URN;
            this.HttpClient = HttpClient;
            this.ParamToken = ParamToken;

            // options.Converters.Add(new StringBooleanConverter());
        }

        protected JsonSerializerOptions options = new JsonSerializerOptions();
        public Parameter[] ParamToken { get; }
        public IHttpClient HttpClient { get; }
        public string CurrentResource { get; }
        public string URN { get; }


        protected ResponseBaseDTO<T> CreateResponse<T>(IRestResponse<T> Response, T?[]? Result) where T : BaseResourceDTO
        {
            return new ResponseBaseDTO<T>
            {
                Content = Response?.Content ?? "",
                StatusCode = Response?.StatusCode ?? System.Net.HttpStatusCode.NotAcceptable,
                IsSuccessful = Response?.IsSuccessful ?? false,
                ErrorMessage = Response?.ErrorMessage ?? "",
                Items = Result
            };
        }

        protected ResponseBaseDTO<T> CreateResponse<T>(IRestResponse Response, T?[]? Result) where T : BaseResourceDTO
        {
            return new ResponseBaseDTO<T>
            {
                Content = Response?.Content ?? "",
                StatusCode = Response?.StatusCode ?? System.Net.HttpStatusCode.NotAcceptable,
                IsSuccessful = Response?.IsSuccessful ?? false,
                ErrorMessage = Response?.ErrorMessage ?? "",
                Items = Result
            };
        }

        protected ResponseBaseListDTO<T> CreatePagedResponse<T>(IRestResponse Response, ResponseBaseListDTO<T>? Result) where T : BaseResourceDTO
        {
            return new ResponseBaseListDTO<T>
            {
                Content = Response?.Content ?? "",
                StatusCode = Response?.StatusCode ?? System.Net.HttpStatusCode.NotAcceptable,
                IsSuccessful = Response?.IsSuccessful ?? false,
                ErrorMessage = Response?.ErrorMessage ?? "",
                page = Result.page,
                content = Result.content
            };
        }

        public virtual void GenerateLogResult<T>(ResponseBaseDTO<T> Response) where T : BaseResourceDTO
        {
            var status = Response.IsSuccessful ? "Sucesso " : "Falha";
            var msg = $"{status} na URN:{URN} do recurso: {CurrentResource}, statuscode: {Response.StatusCode}";

            if (!Response.IsSuccessful)
                msg = $"{msg}, Exception: {Response.ErrorMessage}";

            if (Response.Items?.Length > 0)
                msg = $"{msg}, Content: {JsonSerializer.Serialize(Response.Items)}";
            Logs.Logger.LogError(msg);
        }

        public async virtual Task<ResponseBaseDTO<T>> GetAsync<T>(CancellationToken cancellation) where T : BaseResourceDTO
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}", Method.GET);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync(Request, cancellation);

            T[]? dto = null;
            if (Response.IsSuccessful)
                dto = (JsonSerializer.Deserialize<List<T>>(Response.Content, options)).ToArray();
            
            return CreateResponse(Response, dto);
        }


        public async Task<ResponseBaseDTO<T>> GetAsync<T>(CancellationToken cancellation, int limit, int offset) where T : BaseResourceDTO
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}?limit={limit}&offset={offset}", Method.GET);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync(Request, cancellation);

            T[]? dto = null;
            if (Response.IsSuccessful)
                dto = (JsonSerializer.Deserialize<List<T>>(Response.Content, options)).ToArray();

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<T>> GetByIdAsync<T>(CancellationToken cancellation, string id) where T : BaseResourceDTO
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}/{id}", Method.GET);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync(Request, cancellation);

            T?[]? dto = null;
            if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<T>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<T>> PostAsync<T>(CancellationToken cancellation, T data) where T : BaseResourceDTO
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
                dto = new[] { JsonSerializer.Deserialize<T>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<T>> PutAsync<T>(CancellationToken cancellation, string id, T data) where T : BaseResourceDTO
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
                dto = new[] { JsonSerializer.Deserialize<T>(Response.Content, options) };

            return CreateResponse(Response, dto);
        }

        public async virtual Task<ResponseBaseDTO<T>> DeleteAsync<T>(CancellationToken cancellation, string id) where T : BaseResourceDTO
        {
            var Client = HttpClient.CreateClient();
            var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/{URN}/{id}", Method.DELETE);

            if (ParamToken != null)
            {
                foreach (var param in ParamToken)
                    Request.AddParameter(param);
            }

            var Response = await Client.ExecuteAsync(Request, cancellation);

            T?[]? dto = null;
            /*if (Response.IsSuccessful)
                dto = new[] { JsonSerializer.Deserialize<T>(Response.Content) };
            */

            return CreateResponse(Response, dto);
        }
    }
}
