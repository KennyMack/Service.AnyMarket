using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Entities.CRM;
using Hino.Service.AnyMarket.Entities.General;
using Hino.Service.AnyMarket.Entities.Marketplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    public class OrderDTO : BaseResourceDTO
    {
        public string accountName { get; set; }
        public string marketPlaceId { get; set; }
        public string marketPlaceNumber { get; set; }
        public string marketPlace { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime paymentDate { get; set; }
        public string transmissionStatus { get; set; }
        public string status { get; set; }
        public string marketPlaceStatus { get; set; }
        public double discount { get; set; }
        public double freight { get; set; }
        public double sellerFreight { get; set; }
        public double interestValue { get; set; }
        public double gross { get; set; }
        public double total { get; set; }
        public string deliverStatus { get; set; }
        public int idAccount { get; set; }
        public bool fulfillment { get; set; }

        public OrderShippingDTO shipping { get; set; }
        public OrderAnyMarketAddressDTO anymarketAddress { get; set; }
        public OrderBuyerDTO buyer { get; set; }
        public OrderPaymentDTO[] payments { get; set; }
        public OrderItemDTO[] items { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this);


        public static CROrders ToEntity(OrderDTO order, GEQueueItem queueItem)
        {
            var NewOrder = new CROrders();
            NewOrder.ESTABLISHMENTKEY = queueItem.UNIQUEKEY;
            NewOrder.UNIQUEKEY = queueItem.UNIQUEKEY;
            NewOrder.CREATED = DateTime.Now;
            NewOrder.MODIFIED = DateTime.Now;
            NewOrder.ISACTIVE = true;


            return NewOrder;
        }
    }

    public class OrderShippingDTO: BaseResourceDTO
    {
        public string city { get; set; }
        public string state { get; set; }
        public string stateNameNormalized { get; set; }
        public string country { get; set; }
        public string countryAcronymNormalized { get; set; }
        public string countryNameNormalized { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public string neighborhood { get; set; }
        public string street { get; set; }
        public string zipCode { get; set; }
    }

    public class OrderAnyMarketAddressDTO: BaseResourceDTO
    {
        public string country { get; set; }
        public string state { get; set; }
        public string stateAcronymNormalized { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
        public string neighborhood { get; set; }
        public string address { get; set; }
        public string street { get; set; }
        public string number { get; set; }
    }

    public class OrderBuyerDTO : BaseResourceDTO
    {
        public string name { get; set; }
        public string email { get; set; }
        public string document { get; set; }
        public string documentType { get; set; }
        public string phone { get; set; }
        public string documentNumberNormalized { get; set; }
    }

    public class OrderPaymentDTO: BaseResourceDTO
    {
        public string method { get; set; }
        public string status { get; set; }
        public double value { get; set; }
        public string marketplaceId { get; set; }
        public string paymentMethodNormalized { get; set; }
        public string paymentDetailNormalized { get; set; }
    }

    public class OrderItemDTO : BaseResourceDTO
    {
        public OrderItemProductDTO product { get; set; }
        public OrderItemSKUDTO sku { get; set; }
        public int amount { get; set; }
        public double unit { get; set; }
        public double gross { get; set; }
        public double total { get; set; }
        public double discount { get; set; }
        public string idInMarketPlace { get; set; }
        public int orderItemId { get; set; }
        public bool freeShipping { get; set; }
        public OrderItemStock[] stocks { get; set; }
    }

    public class OrderItemProductDTO : BaseResourceDTO
    {
        public string title { get; set; }
    }

    public class OrderItemSKUDTO : BaseResourceDTO
    {
        public string title { get; set; }
        public string partnerId { get; set; }
        public string ean { get; set; }
    }

    public class OrderItemStock : BaseResourceDTO
    {
        public int stockLocalId { get; set; }
        public double amount { get; set; }
        public string stockName { get; set; }
    }
}
