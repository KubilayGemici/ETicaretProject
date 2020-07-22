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
    public class OrderMap : IEntityBuilder
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasExtended();

                entity.Property(x => x.Quantity).HasMaxLength(15).IsRequired(true);
                entity.Property(x => x.TotalPrice).HasMaxLength(20).IsRequired(true);

                entity
                    .HasOne(e => e.CreatedUserOrder)
                    .WithMany(c => c.CreatedUserOrders)
                    .HasForeignKey(x => x.CreatedUserID);

                entity
                    .HasOne(e => e.ModifiedUserOrder)
                    .WithMany(c => c.ModifiedUserOrders)
                    .HasForeignKey(x => x.ModifiedUserID);

            });
        }
    }
}
