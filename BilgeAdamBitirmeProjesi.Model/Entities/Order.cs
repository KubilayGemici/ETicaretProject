using BilgeAdamBitirmeProjesi.Core.Entity;
using System;

namespace BilgeAdamBitirmeProjesi.Model.Entities
{
    public class Order : CoreEntity
    {
        //Order Details ile bağlantılı yapıcaz.Düzeltilecek.
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public int PaymentType { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }

        public virtual User CreatedUserOrder { get; set; }
        public virtual User ModifiedUserOrder { get; set; }
    }
}
