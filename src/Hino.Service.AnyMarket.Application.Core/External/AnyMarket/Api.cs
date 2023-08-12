using Hino.Service.AnyMarket.Utils.Request;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket
{
    public class Api
    {
        readonly Parameter[] ParamToken;
        readonly IHttpClient HttpClient;
        public Api(
            IConfiguration configuration,
            IHttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
            this.HttpClient.BaseRoute = configuration["BaseUrlAnyMarket"].ToString();
            var GumgaToken = configuration["gumgaToken"].ToString();
            var GumgaOi = configuration["gumgaOi"].ToString();

            ParamToken = new Parameter[] {
                new Parameter("Content-Type", "application/json", ParameterType.HttpHeader),
                new Parameter("gumgaToken", GumgaToken, ParameterType.HttpHeader),
                new Parameter("gumgaOi", GumgaOi, ParameterType.HttpHeader)
            };
        }

        Stocks _Stocks;
        public Stocks Stocks
        {
            get
            {
                _Stocks ??= new Stocks(this.HttpClient, ParamToken);

                return _Stocks;
            }
        }

        StocksLocals _StocksLocals;
        public StocksLocals StocksLocals
        {
            get
            {
                _StocksLocals ??= new StocksLocals(this.HttpClient, ParamToken);

                return _StocksLocals;
            }
        }

        Brands _Brands;
        public Brands Brands
        {
            get
            {
                _Brands ??= new Brands(this.HttpClient, ParamToken);

                return _Brands;
            }
        }
        Categories _Categories;
        public Categories Categories
        {
            get
            {
                _Categories ??= new Categories(this.HttpClient, ParamToken);

                return _Categories;
            }
        }
        VariationsType _VariationsType;
        public VariationsType VariationsType
        {
            get
            {
                _VariationsType ??= new VariationsType(this.HttpClient, ParamToken);

                return _VariationsType;
            }
        }
        VariationsValue _VariationsValue;
        public VariationsValue VariationsValue
        {
            get
            {
                _VariationsValue ??= new VariationsValue(this.HttpClient, ParamToken);

                return _VariationsValue;
            }
        }

        Products _Products;
        public Products Products
        {
            get
            {
                _Products ??= new Products(this.HttpClient, ParamToken);

                return _Products;
            }
        }

        Images _Images;
        public Images Images
        {
            get
            {
                _Images ??= new Images(this.HttpClient, ParamToken);

                return _Images;
            }
        }

        Skus _Skus;
        public Skus Skus
        {
            get
            {
                _Skus ??= new Skus(this.HttpClient, ParamToken);

                return _Skus;
            }
        }
    }
}
