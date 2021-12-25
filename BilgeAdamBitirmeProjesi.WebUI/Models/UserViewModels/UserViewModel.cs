using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.UserViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Email Adressiniz Hatalı")]
        public string Title { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifreniz Hatalı")]
        public string Password { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string Number { get; set; }
    }
}
