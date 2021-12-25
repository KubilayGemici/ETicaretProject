using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using BilgeAdamBitirmeProjesi.Common.DTOs.OrderDetail;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using System;
using System.Collections.Generic;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Order
{
    public class OrderResponse : BaseDto
    {

        public OrderResponse()
        {
            OrderDetails = new HashSet<OrderDetailResponse>();
        }

        public string CustomerName { get; set; }
        public string CustomerSurName { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid ProductId { get; set; }
        public virtual ProductResponse Product { get; set; }
        public Guid UserId { get; set; }

        public ICollection<OrderDetailResponse> OrderDetails { get; set; }
    }
}
