using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Cart
{
    public class CartRequest : BaseDto
    {
        public Guid UserId { get; set; }
    }
}
