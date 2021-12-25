using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using BilgeAdamBitirmeProjesi.WebUI.Models.CartItemViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Models.UserViewModels;
using System;
using System.Collections.Generic;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.CartViewModels
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            CartItems = new HashSet<CartItemViewModel>();
        }

        public Guid Id { get; set; }
        public Status Status { get; set; }
        public Guid UserId { get; set; }
        public virtual UserViewModel User { get; set; }
        public ICollection<CartItemViewModel> CartItems { get; set; }
    }
}
