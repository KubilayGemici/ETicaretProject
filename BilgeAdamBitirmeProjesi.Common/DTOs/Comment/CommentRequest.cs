using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using System;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Comment
{
    public class CommentRequest : BaseDto
    {
        public string CommentText { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
     
    }
}
