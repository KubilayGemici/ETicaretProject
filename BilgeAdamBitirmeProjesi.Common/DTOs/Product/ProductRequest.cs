using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using System;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Product
{
    public class ProductRequest : BaseDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public short UnitsInStock { get; set; }
        public string QuantityPerUnit { get; set; }
        public string ProductDetail { get; set; }
        public string Order { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
