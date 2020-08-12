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
        public Guid OrderId { get; set; }
        public virtual OrderResponse Order { get; set; }
        public Guid ProductId { get; set; }
        public virtual ProductResponse Product { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
    }
}
