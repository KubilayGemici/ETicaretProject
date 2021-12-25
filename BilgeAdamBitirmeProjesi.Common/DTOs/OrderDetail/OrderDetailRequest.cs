using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using System;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.OrderDetail
{
    public class OrderDetailRequest : BaseDto
    {
        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
    }
}
