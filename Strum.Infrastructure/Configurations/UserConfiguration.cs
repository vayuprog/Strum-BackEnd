using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Strum.Core.Entities;

namespace Strum.Infrastructure.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.LastName).HasMaxLength(50);
        builder.Property(x => x.Email).HasMaxLength(50);
        //builder.Property(x => x.PasswordHash).HasMinLenght(50);
        
    }
}