using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.ProductViewModels
{
    public class ProductVM
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public Guid CategoryId { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
