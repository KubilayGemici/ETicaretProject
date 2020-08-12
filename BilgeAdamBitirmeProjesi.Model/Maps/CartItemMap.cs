using BilgeAdamBitirmeProjesi.Core.Map;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Model.Maps.Base;
using Microsoft.EntityFrameworkCore;

namespace BilgeAdamBitirmeProjesi.Model.Maps
{
    class CartItemMap : IEntityBuilder
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<CartItem>(entity =>
            {
                entity.ToTable("CartItems");

                entity.HasExtended();

                entity.Property(x => x.Amount).HasMaxLength(50).IsRequired(true);

            });
        }
    }
}
