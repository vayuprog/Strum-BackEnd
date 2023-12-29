using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Strum.Core.Entities;

namespace Strum.Infrastructure.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.Property(x => x.Text).HasMaxLength(2000);
        builder.Property(x => x.PostImage).HasMaxLength(100000);
        builder.Property(x => x.DatePosted);
        builder.Property(x => x.Likes);
        builder.Property(x => x.Comment);
        builder.Property(x => x.Reposts);
    }
}