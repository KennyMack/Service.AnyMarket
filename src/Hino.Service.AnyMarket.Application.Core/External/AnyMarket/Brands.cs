using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Logs;
using Hino.Service.AnyMarket.Utils.Request;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class Brands: BaseRest
    {
        public Brands(
            IHttpClient HttpClient,
            Parameter[] ParamToken)
            : base("brands", "(Marcas) Brands",
                  HttpClient,
                  ParamToken)
        {
        }

        /*
        public async Task<IEnumerable<BrandDTO>> GetAsync(CancellationToken cancellation)
        {
            try
            {
                var Client = HttpClient.CreateClient();
                var Request = HttpClient.CreateRequest($"{HttpClient.BaseRoute}/brands", Method.GET);

                if (ParamToken != null)
                {
                    foreach (var param in ParamToken)
                        Request.AddParameter(param);
                }

                var Response = await Client.ExecuteAsync(Request, cancellation);
                if (!Response.IsSuccessful)
                {
                    Logger.LogError($"Não foi possível comunicar com a /brands, statuscode: {Response.StatusCode}, message: {Response.ErrorMessage}, Content: {Response.Content} ", Response.ErrorException);
                    return null;
                }
                return JsonSerializer.Deserialize<List<BrandDTO>>(Response.Content);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);

                return null;
            }
        }

        public async Task<IEnumerable<BrandDTO>> PostAsync(CancellationToken cancellation, BrandDTO Brand)
        {
            try
            {
                var Client = Httpclient.CreateClient();
                var Request = Httpclient.CreateRequest($"{Httpclient.BaseRoute}/brands", Method.POST);

                if (ParamToken != null)
                {
                    foreach (var param in ParamToken)
                        Request.AddParameter(param);
                }

                Request.AddJsonBody(Brand);

                var Response = await Client.ExecuteAsync(Request, cancellation);
                if (!Response.IsSuccessful)
                {
                    Logger.LogError($"Não foi possível comunicar com a /brands, statuscode: {Response.StatusCode}, message: {Response.ErrorMessage}, Content: {Response.Content} ", Response.ErrorException);
                    return null;
                }
                return JsonSerializer.Deserialize<List<BrandDTO>>(Response.Content);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);

                return null;
            }
        }

        public async Task<IEnumerable<BrandDTO>> PutAsync(CancellationToken cancellation, BrandDTO Brand)
        {
            try
            {
                var Client = Httpclient.CreateClient();
                var Request = Httpclient.CreateRequest($"{Httpclient.BaseRoute}/brands", Method.POST);

                if (ParamToken != null)
                {
                    foreach (var param in ParamToken)
                        Request.AddParameter(param);
                }

                Request.AddJsonBody(Brand);

                var Response = await Client.ExecuteAsync(Request, cancellation);
                if (!Response.IsSuccessful)
                {
                    Logger.LogError($"Não foi possível comunicar com a /brands, statuscode: {Response.StatusCode}, message: {Response.ErrorMessage}, Content: {Response.Content} ", Response.ErrorException);
                    return null;
                }
                return JsonSerializer.Deserialize<List<BrandDTO>>(Response.Content);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);

                return null;
            }
        }

        public async Task<IEnumerable<BrandDTO>> DeleteAsync(CancellationToken cancellation, BrandDTO Brand)
        {
            try
            {
                var Client = Httpclient.CreateClient();
                var Request = Httpclient.CreateRequest($"{Httpclient.BaseRoute}/brands", Method.POST);

                if (ParamToken != null)
                {
                    foreach (var param in ParamToken)
                        Request.AddParameter(param);
                }

                Request.AddJsonBody(Brand);

                var Response = await Client.ExecuteAsync(Request, cancellation);
                if (!Response.IsSuccessful)
                {
                    Logger.LogError($"Não foi possível comunicar com a /brands, statuscode: {Response.StatusCode}, message: {Response.ErrorMessage}, Content: {Response.Content} ", Response.ErrorException);
                    return null;
                }
                return JsonSerializer.Deserialize<List<BrandDTO>>(Response.Content);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);

                return null;
            }
        }
        */
    }
}
