using BilgeAdamBitirmeProjesi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.OrderDetailViewModels
{
    public class OrderDetailViewModel
    {
        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
