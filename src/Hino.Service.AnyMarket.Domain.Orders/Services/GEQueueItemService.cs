using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Repositories;
using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Services;
using Hino.Service.AnyMarket.Entities.General;
using Hino.Service.AnyMarket.Logs;
using Newtonsoft.Json.Linq;
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

        public async Task<GEQueueItem> CreateQueueItemAsync(long orderId, string token, CancellationToken cancellation)
        {
            try
            {
                var QueueItem = new GEQueueItem
                {
                    ESTABLISHMENTKEY = token,
                    UNIQUEKEY = token,
                    CREATED = DateTime.Now,
                    MODIFIED = DateTime.Now,
                    ISACTIVE = true,
                    IDREFERENCE = orderId,
                    PROCESSED = false,
                    UPLOADED = false,
                    ENTRYNAME = Utils.Constants.VEAnyMarketOrders,
                    TYPE = Utils.Constants.New,
                    NOTE = null,
                    EXCEPTIONCODE = null
                };

                var old = await GEQueueItemRepository.FirstOrDefaultAsync(cancellation, 
                    x => x.IDREFERENCE == orderId &&
                         !x.PROCESSED &&
                         !x.UPLOADED &&
                         x.UNIQUEKEY.ToUpper() == token.ToUpper() &&
                         x.ENTRYNAME.ToUpper() == Utils.Constants.VEAnyMarketOrders.ToUpper());

                if (old != null)
                {
                    old.PROCESSED = false;
                    old.UPLOADED = false;
                    QueueItem = old;
                    GEQueueItemRepository.Update(old);
                }
                else
                    GEQueueItemRepository.Add(QueueItem);

                await GEQueueItemRepository.SaveChangesAsync(cancellation);

                return QueueItem;
            }
            catch (Exception ex)
            {
                var msg = $"Não foi possível salvar o feed id: {orderId} token: {token} na QUEUEITEM, Motivo: {ex.Message}";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return null;
            }
        }

        public async Task<IEnumerable<GEQueueItem>> GetAllPendingProcessedAsync(CancellationToken cancellation)
        {
            try
            {
                return await GEQueueItemRepository.QueryAsync(cancellation,
                    x => x.ENTRYNAME.ToUpper() == Utils.Constants.VEAnyMarketOrders.ToUpper() &&
                        !x.PROCESSED
                    );
            }
            catch (Exception ex)
            {
                var msg = $"Não foi possível buscar fila pendente processamento, Motivo: {ex.Message}";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return new List<GEQueueItem>();
            }
        }

        public async Task<IEnumerable<GEQueueItem>> GetAllProcessedAsync(CancellationToken cancellation)
        {
            try
            {
                return await GEQueueItemRepository.QueryAsync(cancellation,
                    x => x.ENTRYNAME.ToUpper() == Utils.Constants.VEAnyMarketOrders.ToUpper() &&
                        x.PROCESSED && !x.UPLOADED
                    );
            }
            catch (Exception ex)
            {
                var msg = $"Não foi possível buscar fila pendente upload, Motivo: {ex.Message}";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
                return new List<GEQueueItem>();
            }
        }

        public async Task UpdateToUploadedAsync(long IdQueue, CancellationToken cancellation)
        {
            try
            {
                var old = await GEQueueItemRepository.FirstOrDefaultAsync(cancellation,
                    x => x.ID == IdQueue);

                if (old != null)
                {
                    old.UPLOADED = true;
                    GEQueueItemRepository.Update(old);
                    await GEQueueItemRepository.SaveChangesAsync(cancellation);
                }
            }
            catch (Exception ex)
            {
                var msg = $"Não foi possível atualizar o QUEUEITEM id: {IdQueue}, Motivo: {ex.Message}";
                Errors.Add(msg);
                Logger.LogError(msg, ex);
            }

        }
    }
}
