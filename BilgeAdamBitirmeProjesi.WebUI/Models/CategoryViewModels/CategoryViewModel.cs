using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using BilgeAdamBitirmeProjesi.WebUI.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.CategoryViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            Products = new HashSet<ProductViewModel>();
        }
        public Guid Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Status Status { get; set; }

        public ICollection<ProductViewModel> Products { get; set; }
    }
}
