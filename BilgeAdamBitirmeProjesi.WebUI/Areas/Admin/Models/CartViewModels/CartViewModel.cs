using BilgeAdamBitirmeProjesi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.CartViewModels
{
    public class CartViewModel
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
