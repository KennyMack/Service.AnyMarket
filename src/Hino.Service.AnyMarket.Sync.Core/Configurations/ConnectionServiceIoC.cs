using Hino.Service.AnyMarket.DataBase.ContextDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Sync.Core.Configurations
{
    public static class ConnectionServiceIoC
    {
        public static IServiceCollection AddConnection(this IServiceCollection services)
        {
            services.AddDbContext<ServiceContext, OracleServiceContext>(config =>
            {
                config.UseOracle();
            });

            return services;
        }
    }
}
