using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DishesConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.Property(d => d.Price)
                .IsRequired()
                .HasColumnType("decimal(18,4)");
        }
    }
}
