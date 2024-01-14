using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Strum.Core.Entities;
using System.Reflection.Emit;

namespace Strum.Infrastructure.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
                  .HasOne(p => p.User)
                  .WithMany(u => u.Posts)
                  .HasForeignKey(p => p.UserId);

        builder.ToTable("Posts");
        builder.Property(x => x.Text).HasMaxLength(2000);
        builder.Property(x => x.PostImage).HasMaxLength(100000);

        builder.Property(x => x.DatePosted)
            .HasColumnName("DatePosted")
            .IsRequired(); // Add this if DatePosted is required

        builder.Property(x => x.Likes)
            .IsRequired(); // Adjust as needed based on your requirements

        builder.Property(x => x.Comments); // Add constraints based on your requirements

        builder.Property(x => x.Reposts)
            .IsRequired();
    }
}