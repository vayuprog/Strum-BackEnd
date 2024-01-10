using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Strum.Core.Entities;

namespace Strum.Infrastructure.Configurations;

public class MessagesConfiguration: IEntityTypeConfiguration<Messages>
{
    public void Configure(EntityTypeBuilder<Messages> builder)
    {
        builder.ToTable("Messages");
        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.LastName).HasMaxLength(50);
    }
}