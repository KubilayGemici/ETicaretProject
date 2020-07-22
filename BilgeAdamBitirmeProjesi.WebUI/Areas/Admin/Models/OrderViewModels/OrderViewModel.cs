using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.OrderViewModels
{
    public class OrderViewModel
    {
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public int PaymentType { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid ProductId { get; set; }
        public virtual ProductResponse Product { get; set; }
        public Guid UserId { get; set; }
    }
}
