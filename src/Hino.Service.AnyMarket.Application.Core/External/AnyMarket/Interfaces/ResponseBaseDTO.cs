using System.Net;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces
{
    public class ResponseBaseDTO<T> where T : BaseResourceDTO
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }
        
        public T?[]? Items { get; set; }

        public ResponseBaseDTO() 
        {
        }
    }

    public class ResponseBaseWithMessageDTO<T>  where T : BaseResourceDTO
    {
        public string message { get; set; }
        public T data { get; set; }
    }

    public class ResponseBaseListDTO<T> where T : BaseResourceDTO
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }

        public ResponseBaseListPageDTO page { get; set; }
        public T[] content { get; set; }
    }

    public class ResponseBaseListPageDTO
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
    }
}
