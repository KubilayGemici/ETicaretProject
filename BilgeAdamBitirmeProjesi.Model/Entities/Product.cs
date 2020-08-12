using BilgeAdamBitirmeProjesi.Core.Entity;
using System;
using System.Collections.Generic;

namespace BilgeAdamBitirmeProjesi.Model.Entities
{
    public class Product : CoreEntity
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            CartItems = new HashSet<CartItem>();
            OrderDetails = new HashSet<OrderDetail>();
        }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public short UnitsInStock { get; set; }
        public string QuantityPerUnit { get; set; }
        public string ProductDetail { get; set; }
        public string Order { get; set; }

        //Bire çok işlem yapılacak
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        //User olmaması gerek Admin seçimi sağlanabilir.
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual User CreatedUserProduct { get; set; }
        public virtual User ModifiedUserProduct { get; set; }
    }
}
