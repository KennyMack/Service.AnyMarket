using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    internal class StockBalanceReturnDTO
    {
        public StockKeepingUnitDTO stockKeepingUnit { get; set; }
        public StockLocalDTO stockLocal { get; set; }

        public double amount { get; set; }
        public double price { get; set; }
        public int additionalTime { get; set; }

        public StockBalanceDTO? ToStockBalanceDTO()
        {
            return new StockBalanceDTO
            {
                id = stockKeepingUnit.id,
                partnerId = stockKeepingUnit.partnerId,
                quantity = Convert.ToInt32(Math.Ceiling(amount)),
                cost = Convert.ToInt32(Math.Ceiling(price)),
                additionalTime = additionalTime,
                stockLocalId = stockLocal?.id ?? 0
            };
        }
    }

    internal class StockKeepingUnitDTO
    {
        public long id { get; set; }
        public string partnerId { get; set; }

    }
}
