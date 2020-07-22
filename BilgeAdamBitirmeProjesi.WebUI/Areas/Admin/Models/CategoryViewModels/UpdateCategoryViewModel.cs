using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.CategoryViewModels
{
    public class UpdateCategoryViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }
        public Status Status { get; set; }
    }
}
