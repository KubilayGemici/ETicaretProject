using BilgeAdamBitirmeProjesi.Core.Map;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Model.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Model.Maps
{
    public class ProductMap : IEntityBuilder
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasExtended();

                entity.Property(x => x.ProductName).HasMaxLength(50).IsRequired(true);
                entity.Property(x => x.Description).HasMaxLength(255).IsRequired(true);
                entity.Property(x => x.Price).HasMaxLength(10).IsRequired(true);
                entity.Property(x => x.Image).IsRequired(true);


                entity
                    .HasOne(e => e.CreatedUserProduct)
                    .WithMany(c => c.CreatedUserProducts)
                    .HasForeignKey(x => x.CreatedUserID);

                entity
                    .HasOne(e => e.ModifiedUserProduct)
                    .WithMany(c => c.ModifiedUserProducts)
                    .HasForeignKey(x => x.ModifiedUserID);

                entity
                    .HasOne(e => e.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(x => x.CategoryId);
            });
        }
    }
}
