using BilgeAdamBitirmeProjesi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Model.Entities
{
    public class Comment : CoreEntity
    {
        public string CommentText { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual User CreatedUserComment { get; set; }
        public virtual User ModifiedUserComment { get; set; }

    }
}
