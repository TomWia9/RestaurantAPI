using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(30);
    }
}
}
