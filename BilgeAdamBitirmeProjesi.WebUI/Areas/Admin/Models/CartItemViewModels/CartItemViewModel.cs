using BilgeAdamBitirmeProjesi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.CartItemViewModels
{
    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Amount { get; set; }
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; }

    }
}
