using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.CartItem
{
    public class CartItemRequest : BaseDto
    {
        public int Amount { get; set; }
    }
}
