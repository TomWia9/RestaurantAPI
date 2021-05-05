using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RestaurantsConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(r => r.Description)
                .HasMaxLength(500);

            builder.Property(r => r.Category)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(r => r.HasDelivery)
                .IsRequired();

            builder.Property(r => r.ContactEmail)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(r => r.ContactNumber)
                .IsRequired()
                .HasMaxLength(16);

            builder.HasOne(r => r.Address)
                .WithOne(a => a.Restaurant)
                .HasForeignKey<Address>(a => a.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Dishes)
                .WithOne(d => d.Restaurant)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
