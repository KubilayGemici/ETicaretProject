using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using System;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Comment
{
    public class CommentResponse : BaseDto
    {
        public string CommentText { get; set; }
        public Guid ProductId { get; set; }
        public ProductResponse Product { get; set; }
        public Guid UserId { get; set; }
        public virtual UserResponse User { get; set; }
    }
}
