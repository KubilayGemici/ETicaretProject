using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using System;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Order
{
    public class OrderRequest : BaseDto
    {

        public string CustomerName { get; set; }
        public string CustomerSurName { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
