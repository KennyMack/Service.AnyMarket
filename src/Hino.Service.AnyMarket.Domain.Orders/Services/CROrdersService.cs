using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Repositories;
using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Domain.Orders.Services
{
    public class CROrdersService: ICROrdersService
    {
        public List<string> Errors { get; set; }

        readonly ICROrdersRepository Repository;

        public CROrdersService(ICROrdersRepository pRepository)
        {
            Errors = new List<string>();
            Repository = pRepository;
        }

        public void Dispose()
        {
            Repository?.Dispose();
        }
    }
}
