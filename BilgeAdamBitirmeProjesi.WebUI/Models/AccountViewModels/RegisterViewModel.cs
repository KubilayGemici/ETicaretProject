using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Adress { get; set; }
        public string Number { get; set; }
        [Required(ErrorMessage = "E-Posta Adresi Gereklidir.")]
        [Display(Name = "E-Posta")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parola Gereklidir.")]
        [Display(Name = "Parola")]
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastIPAdress { get; set; }
        public Status Status { get; set; }
    }
}
