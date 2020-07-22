using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using BilgeAdamBitirmeProjesi.Common.DTOs.Category;
using BilgeAdamBitirmeProjesi.Common.DTOs.Comment;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using System;
using System.Collections.Generic;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Product
{
    //Response hepsi ilişkili olacak.
    public class ProductResponse : BaseDto
    {
        public ProductResponse()
        {
            Comments = new HashSet<CommentResponse>();
        }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public short UnitsInStock { get; set; }
        public string QuantityPerUnit { get; set; }
        public string ProductDetail { get; set; }
        public string Order { get; set; }
        public int ViewCount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid CategoryId { get; set; }
        public virtual CategoryResponse Category { get; set; }
        public Guid UserId { get; set; }
        public virtual UserResponse User { get; set; }
        //Listeler çoğul
        public ICollection<CommentResponse> Comments { get; set; }
    }
}
