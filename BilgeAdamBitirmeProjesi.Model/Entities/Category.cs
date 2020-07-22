using BilgeAdamBitirmeProjesi.Core.Entity;
using System.Collections.Generic;

namespace BilgeAdamBitirmeProjesi.Model.Entities
{
    public class Category : CoreEntity
    {
        //Bire çok bağlantı ayağı kaldırdık.
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
        public User CreatedUserCategory { get; set; }
        public User ModifiedUserCategory { get; set; }

    }
}
