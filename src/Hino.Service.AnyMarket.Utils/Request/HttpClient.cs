using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Utils.Request
{
    public interface IHttpClient
    {
        string BaseRoute { get; set; }

        RestClient CreateClient();

        RestRequest CreateRequest(string pUri, Method pMethod);
    }

    public class HttpClient : IHttpClient
    {
        public string BaseRoute { get; set; }

        public RestClient CreateClient() => 
            new(BaseRoute);

        public RestRequest CreateRequest(string pUri, Method pMethod) => 
            new(pUri, pMethod);
    }
}
