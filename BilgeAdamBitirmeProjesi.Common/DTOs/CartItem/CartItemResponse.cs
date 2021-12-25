using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using BilgeAdamBitirmeProjesi.Common.DTOs.Cart;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using System;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.CartItem
{
    public class CartItemResponse : BaseDto
    {
        public Guid ProductId { get; set; }
        public virtual ProductResponse Product { get; set; }
        public decimal Amount { get; set; }
        public Guid CartId { get; set; }
        public virtual CartResponse Cart { get; set; }
        
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public decimal Total { get; set; }
    }
}
