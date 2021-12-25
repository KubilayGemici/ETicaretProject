using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using BilgeAdamBitirmeProjesi.WebUI.Models.CartViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Models.ProductViewModels;
using System;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.CartItemViewModels
{
    public class CartItemViewModel
    {
        
         public Guid Id { get; set; }
        public Status Status { get; set; }
        public Guid ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }
        public int Amount { get; set; }
        public Guid CartId { get; set; }
        public virtual CartViewModel Cart { get; set; }
    }
}
