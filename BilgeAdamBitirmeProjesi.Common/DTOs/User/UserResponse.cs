using BilgeAdamBitirmeProjesi.Common.Client.Models;
using BilgeAdamBitirmeProjesi.Common.DTOs.Base;
using BilgeAdamBitirmeProjesi.Common.DTOs.Comment;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using System;
using System.Collections.Generic;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.User
{
    public class UserResponse : BaseDto
    {
        public UserResponse()
        {

            Products = new HashSet<ProductResponse>();
            Comments = new HashSet<CommentResponse>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }
        public string Number { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastIPAdress { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<ProductResponse> Products { get; set; }
        public virtual ICollection<CommentResponse> Comments { get; set; }

        public GetAccessToken AccessToken { get; set; }
    }
}
