using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using BilgeAdamBitirmeProjesi.Common.DTOs.CartItem;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Cart
{
    public class CartResponse : BaseDto
    {
        public CartResponse()
        {
            CartItems = new HashSet<CartItemResponse>();
        }
        public ICollection<CartItemResponse> CartItems { get; set; }

        public Guid UserId { get; set; }
        public virtual UserResponse User { get; set; }
    }
}
