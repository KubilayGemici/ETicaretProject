using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.CartItem
{
    public class CartItemRequest : BaseDto
    {
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
    }
}
