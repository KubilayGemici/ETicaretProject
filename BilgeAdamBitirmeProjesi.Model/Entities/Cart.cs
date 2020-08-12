using BilgeAdamBitirmeProjesi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Model.Entities
{
    public class Cart : CoreEntity
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }
        public ICollection<CartItem> CartItems { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
