using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using System;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Order
{
    public class OrderResponse : BaseDto
    {
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public int PaymentType { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid ProductId { get; set; }
        public virtual ProductResponse Product { get; set; }
        public Guid UserId { get; set; }
    }
}
