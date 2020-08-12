using BilgeAdamBitirmeProjesi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Model.Entities
{
    public class CartItem : CoreEntity
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Amount { get; set; }
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
