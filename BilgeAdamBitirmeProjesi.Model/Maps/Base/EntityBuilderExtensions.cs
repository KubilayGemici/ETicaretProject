using BilgeAdamBitirmeProjesi.Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Model.Maps.Base
{
    public static class EntityBuilderExtensions
    {
        public static void HasExtended<T>(this EntityTypeBuilder<T> entity) where T : CoreEntity
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Status).IsRequired(true);

            //False yapma nedenimiz oluştururken tarih verme zorunluluğu olmasın dedik. => public DateTime? CreatedDate { get; set; }
            entity.Property(x => x.CreatedDate).IsRequired(false);
            entity.Property(x => x.CreatedComputerName).HasMaxLength(250).IsRequired(false);
            entity.Property(x => x.CreatedIP).HasMaxLength(15).IsRequired(false);
            //Admin otomatik gelsin diye false yaptım.
            entity.Property(x => x.CreatedUserID).IsRequired(false);


            entity.Property(x => x.ModifiedDate).IsRequired(false);
            entity.Property(x => x.ModifiedComputerName).HasMaxLength(250).IsRequired(false);
            entity.Property(x => x.ModifiedIP).HasMaxLength(15).IsRequired(false);
            entity.Property(x => x.ModifiedUserID).IsRequired(false);
        }
    }
}
