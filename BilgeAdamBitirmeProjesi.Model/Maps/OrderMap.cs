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

                entity.Property(x => x.Address).HasMaxLength(200).IsRequired(true);
                entity.Property(x => x.TotalPrice).HasMaxLength(20).IsRequired(true);
                entity.Property(x => x.CustomerName).HasMaxLength(20).IsRequired(true);
                entity.Property(x => x.CustomerSurName).HasMaxLength(20).IsRequired(true);

            });
        }
    }
}
