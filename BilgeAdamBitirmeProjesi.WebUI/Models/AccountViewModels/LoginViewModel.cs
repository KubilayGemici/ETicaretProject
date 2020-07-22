using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-Posta Adresi Gereklidir.")]
        [Display(Name ="E-Posta")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parola Gereklidir.")]
        [Display(Name = "Parola")]
        public string Password { get; set; }

    }
}
