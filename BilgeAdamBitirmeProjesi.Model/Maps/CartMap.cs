using BilgeAdamBitirmeProjesi.Core.Map;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Model.Maps.Base;
using Microsoft.EntityFrameworkCore;

namespace BilgeAdamBitirmeProjesi.Model.Maps
{
    class CartMap : IEntityBuilder
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<Cart>(entity =>
            {
                entity.ToTable("Carts");

                entity.HasExtended();

                entity
               .HasOne(e => e.User)
               .WithMany(c => c.Carts)
               .HasForeignKey(x => x.UserId);
            });
        }
    }
}
