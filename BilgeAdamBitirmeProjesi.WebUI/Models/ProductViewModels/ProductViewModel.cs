using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using BilgeAdamBitirmeProjesi.WebUI.Models.CartItemViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Models.CategoryViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Models.OrderViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.ProductViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            CartItems = new HashSet<CartItemViewModel>();
            OrderDetails = new HashSet<OrderDetailViewModel>();
        }

        public Guid Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public short UnitsInStock { get; set; }
        public int ViewCount { get; set; }
        public int QuantityPerUnit { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Guid CategoryId { get; set; }
        public virtual CategoryViewModel Category { get; set; }
        public Guid UserId { get; set; }
        public virtual UserViewModel User { get; set; }
        public Status Status { get; set; }

        public ICollection<CartItemViewModel> CartItems { get; set; }
        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
