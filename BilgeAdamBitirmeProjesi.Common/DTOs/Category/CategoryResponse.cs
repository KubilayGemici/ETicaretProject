using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using System;
using System.Collections.Generic;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Category
{
    public class CategoryResponse : BaseDto
    {
        public CategoryResponse()
        {
            Products = new HashSet<ProductResponse>();
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        //Bağlantı yapıldı.
        public ICollection<ProductResponse> Products { get; set; }
    }
}
