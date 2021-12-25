using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.CategoryViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.ProductViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public short UnitsInStock { get; set; }
        public int ViewCount { get; set; }
        public int QuantityPerUnit { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Guid CategoryId { get; set; }
        public virtual CategoryViewModel Category { get; set; }
        public Guid UserId { get; set; }
        public virtual UserViewModel User { get; set; }
        public Status Status { get; set; }
    }
}
