using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.OrderDetail
{
    public class OrderDetailRequest : BaseDto
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
    }
}
