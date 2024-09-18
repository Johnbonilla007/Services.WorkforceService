using System;
using Services.NetCore.Crosscutting.Core;

namespace Services.NetCore.Crosscutting.Dtos.Produce
{
    public class ProduceRequest : RequestBase
    {
        public int IdProducto { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }

        public DateTime DatePurchase { get; set; }
    }
}