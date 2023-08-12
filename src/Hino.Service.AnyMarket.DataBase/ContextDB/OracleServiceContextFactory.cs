using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.DataBase.ContextDB
{
    public interface IOracleServiceContextFactory : IDesignTimeDbContextFactory<OracleServiceContext>
    {

    }

    public class OracleServiceContextFactory : IOracleServiceContextFactory
    {
        public OracleServiceContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ServiceContext>();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionStringName = config["DefaultConnectionString"].ToString();

            var connectionString = config.GetConnectionString(connectionStringName);

            builder.UseOracle(connectionString);

            return new OracleServiceContext(builder.Options);
        }
    }
}
