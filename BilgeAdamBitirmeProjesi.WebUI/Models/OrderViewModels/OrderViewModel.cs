using BilgeAdamBitirmeProjesi.Common.Client.Enums;
using BilgeAdamBitirmeProjesi.WebUI.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.OrderViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            OrderDetails = new HashSet<OrderDetailViewModel>();
        }

        public Guid Id { get; set; }
        public Status Status { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurName { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }
        public Guid UserId { get; set; }

        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
