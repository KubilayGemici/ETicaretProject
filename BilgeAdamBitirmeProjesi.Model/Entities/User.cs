﻿using BilgeAdamBitirmeProjesi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Model.Entities
{
    public class User : CoreEntity
    {
        public User()
        {
            //Product kaldırılacak.
            Products = new HashSet<Product>();
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
            CreatedUserProducts = new HashSet<Product>();
            ModifiedUserProducts = new HashSet<Product>();
            CreatedUserCategories = new HashSet<Category>();
            ModifiedUserCategories = new HashSet<Category>();
            CreatedUserComments = new HashSet<Comment>();
            ModifiedUserComments = new HashSet<Comment>();
            CreatedUsers = new HashSet<User>();
            ModifiedUsers = new HashSet<User>();

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        //Adres eklenicek.
        //Telefon numarası kısmı olacak.
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastIPAdress { get; set; }

        public virtual User CreatedUser { get; set; }
        public virtual User ModifiedUser { get; set; }

        public virtual ICollection<User> CreatedUsers { get; set; }
        public virtual ICollection<User> ModifiedUsers { get; set; }
        //Product
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> CreatedUserProducts { get; set; }
        public virtual ICollection<Product> ModifiedUserProducts { get; set; }
        public virtual ICollection<Category> CreatedUserCategories { get; set; }
        public virtual ICollection<Category> ModifiedUserCategories { get; set; }
        public virtual ICollection<Comment> CreatedUserComments { get; set; }
        public virtual ICollection<Comment> ModifiedUserComments { get; set; }
        public virtual ICollection<Order> CreatedUserOrders { get; set; }
        public virtual ICollection<Order> ModifiedUserOrders { get; set; }
    }
}
