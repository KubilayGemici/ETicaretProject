using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using BilgeAdamBitirmeProjesi.Common.DTOs.Cart;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.CartItem
{
    public class CartItemResponse : BaseDto
    {
        public Guid ProductId { get; set; }
        public virtual ProductResponse Product { get; set; }
        public int Amount { get; set; }
        public Guid CartId { get; set; }
        public virtual CartResponse Cart { get; set; }
    }
}
