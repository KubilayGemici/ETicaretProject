using BilgeAdamBitirmeProjesi.Core.Map;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Model.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Model.Maps
{
    public class UserMap : IEntityBuilder
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasExtended();

                entity.Property(x => x.FirstName).HasMaxLength(50).IsRequired(true);
                entity.Property(x => x.LastName).HasMaxLength(50).IsRequired(true);
                entity.Property(x => x.Title).HasMaxLength(50).IsRequired(true);
                entity.Property(x => x.ImageUrl).HasMaxLength(250).IsRequired(false);
                entity.Property(x => x.Email).HasMaxLength(50).IsRequired(true);
                entity.Property(x => x.Password).HasMaxLength(12).IsRequired(true);
                entity.Property(x => x.LastLogin).HasMaxLength(50).IsRequired(false);
                entity.Property(x => x.LastIPAdress).HasMaxLength(250).IsRequired(false);


                //Karşılık belirtme durumunu sağladım.
                entity
                    .HasOne(e => e.CreatedUser)
                    .WithMany(c => c.CreatedUsers)
                    .HasForeignKey(x => x.CreatedUserID);
                entity
                    .HasOne(e => e.ModifiedUser)
                    .WithMany(c => c.ModifiedUsers)
                    .HasForeignKey(x => x.ModifiedUserID);

            });
        }
    }
}
