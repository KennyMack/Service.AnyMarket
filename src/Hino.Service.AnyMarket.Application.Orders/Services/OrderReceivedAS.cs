using Hino.Service.AnyMarket.Application.Core.External.AnyMarket;
using Hino.Service.AnyMarket.Application.Orders.Interfaces;
using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Services;
using Hino.Service.AnyMarket.Domain.Orders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Orders.Services
{
    public class OrderReceivedAS : IOrderReceivedAS
    {
        public List<string> Errors
        {
            get
            {
                var list = new List<string>();
                list.AddRange(CROrdersService.Errors.ToArray());
                list.AddRange(CROrderItemsService.Errors.ToArray());
                list.AddRange(GEQueueItemService.Errors.ToArray());

                return list;
            }
        }

        readonly ICROrdersService CROrdersService;
        readonly ICROrderItemsService CROrderItemsService;
        readonly IGEQueueItemService GEQueueItemService;
        readonly Api ApiAnyMarket;

        public OrderReceivedAS(ICROrdersService cROrdersService,
            ICROrderItemsService cROrderItemsService,
            IGEQueueItemService gEQueueItemService,
            Api apiAnyMarket)
        {
            CROrdersService = cROrdersService;
            CROrderItemsService = cROrderItemsService;
            GEQueueItemService = gEQueueItemService;
            ApiAnyMarket = apiAnyMarket;
        }

        public void Dispose()
        {
            CROrdersService?.Dispose();
            CROrderItemsService?.Dispose();
            GEQueueItemService?.Dispose();
        }

        public async Task ReceiveNewOrdersAsync(CancellationToken cancellationToken)
        {
            // get new orders from AnyMarket API
            //await ApiAnyMarket.GetOrdersAsync(cancellationToken);

            // get new order items from AnyMarket API
            //await ApiAnyMarket.GetOrderItemsAsync(cancellationToken);

            // Create Queue Item Entity

            

        }
    }
}
