using BilgeAdamBitirmeProjesi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Model.Entities
{
    public class OrderDetail : CoreEntity
    {

        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
