using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Logs;
using Hino.Service.AnyMarket.Utils.Request;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class Categories : BaseRest
    {
        public Categories(IHttpClient HttpClient,
            Parameter[] ParamToken)
            : base("categories", "(Marcas) Brands",
                  HttpClient,
                  ParamToken)
        {
        }
    }
}
