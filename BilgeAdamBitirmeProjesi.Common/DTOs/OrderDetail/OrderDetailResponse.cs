using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using BilgeAdamBitirmeProjesi.Common.DTOs.Order;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.OrderDetail
{
    public class OrderDetailResponse : BaseDto
    {
        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public virtual OrderResponse Order { get; set; }
        public Guid ProductId { get; set; }
        public virtual ProductResponse Product { get; set; }


    }
}
