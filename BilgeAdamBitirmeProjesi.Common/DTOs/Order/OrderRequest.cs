using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using System;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Order
{
    public class OrderRequest : BaseDto
    {
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public int PaymentType { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
