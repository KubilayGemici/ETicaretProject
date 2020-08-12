using BilgeAdamBitirmeProjesi.Core.Entity;
using System;
using System.Collections.Generic;

namespace BilgeAdamBitirmeProjesi.Model.Entities
{
    public class Order : CoreEntity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public int PaymentType { get; set; }
        public DateTime OrderDate { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
