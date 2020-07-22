using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.ProductViewModels
{
    public class UpdateProductViewModel
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

        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public Status Status { get; set; }
    }
}
