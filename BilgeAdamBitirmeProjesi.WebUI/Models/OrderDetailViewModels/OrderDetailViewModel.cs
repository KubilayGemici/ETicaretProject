using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using BilgeAdamBitirmeProjesi.WebUI.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.OrderViewModels
{
    public class OrderDetailViewModel
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public virtual OrderViewModel Order { get; set; }
        public Guid ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }

    }
}
