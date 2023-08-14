using Hino.Service.AnyMarket.Application.Core.External.AnyMarket;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Orders.Interfaces;
using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Services;
using Hino.Service.AnyMarket.Domain.Orders.Services;
using Hino.Service.AnyMarket.Entities.General;
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
            Errors.Clear();
            Logs.Logger.LogInformation($"Processamento de novos pedidos do feed");

            // get new orders from AnyMarket API
            await FetchFeedOrdersAsync(cancellationToken);



            // get new order items from AnyMarket API
            //await ApiAnyMarket.GetOrderItemsAsync(cancellationToken);

            // Create Queue Item Entity

            await ConfirmOrdersReceivedAsync(cancellationToken);

        }

        public async Task DownloadOrdersFromApiAsync(CancellationToken cancellationToken)
        {
            Logs.Logger.LogInformation($"Baixando pedidos recebidos do feed para a CRORDERS");
            var ListPendingProcessed = await GEQueueItemService.GetAllProcessedAsync(cancellationToken);

            Logs.Logger.LogInformation($"Foram encontrados {ListPendingProcessed.Count()} pedidos para baixar no feed");

            foreach (var item in ListPendingProcessed)
            {
                Logs.Logger.LogInformation($"Baixando o pedido id: {item.IDREFERENCE} token: {item.UNIQUEKEY} no feed");
                try
                {
                    var processedDTO = new FeedOrderDTO
                    {
                        id = item.IDREFERENCE,
                        token = item.UNIQUEKEY,
                    };

                }
                catch (Exception ex)
                {
                    Logs.Logger.LogError($"Falha na URN:{ApiAnyMarket.FeedOrders.URN} do recurso: {ApiAnyMarket.FeedOrders.CurrentResource}, Exception: {ex.Message}", ex);
                }
            }
        }

        public async Task ConfirmOrdersReceivedAsync(CancellationToken cancellationToken)
        {
            Logs.Logger.LogInformation($"Confirmando pedidos recebidos no feed");
            var ListProcessed = await GEQueueItemService.GetAllProcessedAsync(cancellationToken);

            Logs.Logger.LogInformation($"Foram encontrados {ListProcessed.Count()} pedidos para confirmar no feed");
            foreach (var item in ListProcessed)
            {
                Logs.Logger.LogInformation($"Confirmando o pedido id: {item.IDREFERENCE} token: {item.UNIQUEKEY} no feed");
                try
                {
                    var processedDTO = new FeedOrderDTO
                    {
                        id = item.IDREFERENCE,
                        token = item.UNIQUEKEY,
                    };

                    //var ResultOrdersFeed = await ApiAnyMarket.FeedOrders.PutAsync(cancellationToken, processedDTO.id.ToString(), processedDTO);

                    //ApiAnyMarket.FeedOrders.GenerateLogResult(ResultOrdersFeed);
                }
                catch (Exception ex)
                {
                    Logs.Logger.LogError($"Falha na URN:{ApiAnyMarket.FeedOrders.URN} do recurso: {ApiAnyMarket.FeedOrders.CurrentResource}, Exception: {ex.Message}", ex);
                }
            }
        }

        public async Task CreateQueueItemAsync(FeedOrderDTO feed, CancellationToken cancellationToken) =>
            await GEQueueItemService.CreateQueueItemAsync(feed.id ?? 0, feed.token, cancellationToken);

        public async Task FetchFeedOrdersAsync(CancellationToken cancellationToken)
        {
            Logs.Logger.LogInformation($"Buscando os pedidos do feed");

            try
            {
                var ResultOrdersFeed = await ApiAnyMarket.FeedOrders.GetAsync<FeedOrderDTO>(cancellationToken);

                ApiAnyMarket.FeedOrders.GenerateLogResult(ResultOrdersFeed);
                
                if (ResultOrdersFeed.IsSuccessful &&
                    ResultOrdersFeed.Items?.Length > 0)
                {
                    Logs.Logger.LogInformation($"Foram encontrados {ResultOrdersFeed.Items?.Length} pedidos no feed");

                    foreach (var item in ResultOrdersFeed.Items)
                    {
                        Logs.Logger.LogInformation($"Salvando o feed pedido: {item.id} token: {item.token} do feed de pedidos na tabela GEQUEUEITEM");
                        await CreateQueueItemAsync(item, cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Logger.LogError($"Falha na URN:{ApiAnyMarket.FeedOrders.URN} do recurso: {ApiAnyMarket.FeedOrders.CurrentResource}, Exception: {ex.Message}", ex);
            }
        }
    }
}
