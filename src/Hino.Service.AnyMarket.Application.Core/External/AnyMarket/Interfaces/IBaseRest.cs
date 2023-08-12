using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Logs;
using Hino.Service.AnyMarket.Utils.Request;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces
{
    public interface IBaseRest
    {
        Parameter[] ParamToken { get; }
        IHttpClient HttpClient { get; }
        string CurrentResource { get; }
        string URN { get; }
        Task<ResponseBaseDTO<T>> GetByIdAsync<T>(CancellationToken cancellation, string id) where T : BaseResourceDTO;
        Task<ResponseBaseDTO<T>> GetAsync<T>(CancellationToken cancellation) where T : BaseResourceDTO;
        Task<ResponseBaseDTO<T>> GetAsync<T>(CancellationToken cancellation, int limit, int offset) where T : BaseResourceDTO;
        Task<ResponseBaseDTO<T>> PostAsync<T>(CancellationToken cancellation, T data) where T : BaseResourceDTO;
        Task<ResponseBaseDTO<T>> PutAsync<T>(CancellationToken cancellation, string id, T data) where T : BaseResourceDTO;
        Task<ResponseBaseDTO<T>> DeleteAsync<T>(CancellationToken cancellation, string id) where T : BaseResourceDTO;
    }
}
