using Hino.Service.AnyMarket.Application.Core.External.AnyMarket;
using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.IoC.Services;
using Hino.Service.AnyMarket.Utils.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.IoC
{
    public static class BindingIoC
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<ServiceContext>(config =>
            {
                var factory = new OracleServiceContextFactory();
                config.UseOracle(factory.CreateDbContext(null).Database.GetConnectionString());
            });

            services.AddScoped<IHttpClient, Utils.Request.HttpClient>();

            services.AddScoped<Api>();

            /*services.AddDbContext<ServiceContext, OracleServiceContext>(config =>
            {
                config.UseOracle("");
            });*/

            //services.AddScoped<ServiceContext>();

            services.AddMarketplaceInstances();

        }
    }
}
