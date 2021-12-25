using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.UserViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string Number { get; set; }

    }
}
