using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Entities.Fiscal;
using Hino.Service.AnyMarket.Entities.Marketplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    public class StockBalanceDTO: BaseResourceDTO
    {
        public string partnerId { get; set; }
        public int quantity { get; set; }
        public int cost { get; set; }
        public int additionalTime { get; set; }
        public long stockLocalId { get; set; }

        public static StockBalanceDTO[] FromEntity(FSVWProdSaldoEstGrupoDisponivel[] estoques)
        {
            return estoques
                .Select(x => new StockBalanceDTO
                {
                    partnerId = x.CODPRODUTO,
                    quantity = Convert.ToInt32(Math.Ceiling(x.SALDODISPONIVEL)),
                    cost = Convert.ToInt32(Math.Ceiling(x.CUSTO)),
                    additionalTime = x.LEADTIME,
                    stockLocalId = x.IDAPIESTOQUE ?? 0,
                    id = x.IDAPISKU
                }).ToArray();
        }
    }
}
