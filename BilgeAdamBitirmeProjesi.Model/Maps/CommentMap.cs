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
    public class CommentMap : IEntityBuilder
    {
        public void Build(ModelBuilder builder)
        {
            builder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments");

                entity.HasExtended();

                entity.Property(x => x.CommentText).IsRequired(true);

                entity
                .HasOne(e => e.CreatedUserComment)
                .WithMany(c => c.CreatedUserComments)
                .HasForeignKey(x => x.CreatedUserID);

                entity
                    .HasOne(e => e.ModifiedUserComment)
                    .WithMany(c => c.ModifiedUserComments)
                    .HasForeignKey(x => x.ModifiedUserID);

                entity
                .HasOne(e => e.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(x => x.UserId);

            });
        }
    }
}
