using BilgeAdamBitirmeProjesi.Core.Map;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Model.Maps.Base;
using Microsoft.EntityFrameworkCore;

namespace BilgeAdamBitirmeProjesi.Model.Maps
{
    public class CategoryMap : IEntityBuilder
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                entity.HasExtended();

                entity.Property(x => x.CategoryName).HasMaxLength(50).IsRequired(true);
                entity.Property(x => x.Description).HasMaxLength(255).IsRequired(true);

                //Karşılık belirtme durumunu sağladım.
                entity
                .HasOne(e => e.CreatedUserCategory)
                .WithMany(c => c.CreatedUserCategories)
                .HasForeignKey(d => d.CreatedUserID);

                entity
                    .HasOne(e => e.ModifiedUserCategory)
                    .WithMany(c => c.ModifiedUserCategories)
                    .HasForeignKey(d => d.ModifiedUserID);
            });

        }
    }
}
