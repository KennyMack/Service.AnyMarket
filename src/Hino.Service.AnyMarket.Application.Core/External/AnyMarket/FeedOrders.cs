using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Logs;
using Hino.Service.AnyMarket.Utils.Request;
using RestSharp;
using System.Text.Json;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class FeedOrders : BaseRest
    {
        public FeedOrders(
            IHttpClient HttpClient,
            Parameter[] ParamToken)
            : base("orders/feeds", "(Feed pedidos) orders feed",
                  HttpClient,
                  ParamToken)
        {
        }
    }
}
