using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Repositories;
using Hino.Service.AnyMarket.Domain.Orders.Interfaces.Services;

namespace Hino.Service.AnyMarket.Domain.Orders.Services
{
    public class CROrderItemsService : ICROrderItemsService
    {
        public List<string> Errors { get; set; }

        readonly ICROrderItemsRepository Repository;

        public CROrderItemsService(ICROrderItemsRepository pRepository)
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
