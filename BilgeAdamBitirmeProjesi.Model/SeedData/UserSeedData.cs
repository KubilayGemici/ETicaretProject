using BilgeAdamBitirmeProjesi.Core.Entity.Enums;
using BilgeAdamBitirmeProjesi.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BilgeAdamBitirmeProjesi.Model.SeedData
{
    public class UserSeedData : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Status = Status.Active,
                    Email = "admin@admin.com",
                    Password = "123",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Title = "Admin",
                    ImageUrl = "/",
                    LastLogin = DateTime.Now,
                    LastIPAdress = "94.54.234.138"
                });
        }
    }
}
