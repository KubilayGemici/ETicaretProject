using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.ProductViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public short UnitsInStock { get; set; }
        public int ViewCount { get; set; }

        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public Status Status { get; set; }
    }
}
