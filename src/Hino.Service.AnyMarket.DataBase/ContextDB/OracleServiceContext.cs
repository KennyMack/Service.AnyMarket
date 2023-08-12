using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.DataBase.ContextDB
{
    public class OracleServiceContext : ServiceContext
    {
        public OracleServiceContext(DbContextOptions<ServiceContext> options) :
            base(options)
        {
        }
    }
}
