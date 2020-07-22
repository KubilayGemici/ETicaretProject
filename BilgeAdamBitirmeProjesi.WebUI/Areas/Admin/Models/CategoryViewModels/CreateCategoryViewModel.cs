using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.CategoryViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }
        public Status Status { get; set; }
    }
}
