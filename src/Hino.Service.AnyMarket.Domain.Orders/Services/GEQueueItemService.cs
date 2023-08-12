using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Repositories;
using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Domain.Orders.Services
{
    public class GEQueueItemService : IGEQueueItemService
    {
        public List<string> Errors { get; set; }

        readonly IGEQueueItemRepository GEQueueItemRepository;

        public GEQueueItemService(IGEQueueItemRepository pRepository)
        {
            Errors = new List<string>();
            GEQueueItemRepository = pRepository;
        }

        public void Dispose()
        {
            GEQueueItemRepository?.Dispose();
        }
    }
}
