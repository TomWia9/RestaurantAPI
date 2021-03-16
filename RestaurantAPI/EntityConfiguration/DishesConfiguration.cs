using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAPI.Models;

namespace RestaurantAPI.EntityConfiguration
{
    public class DishesConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(d => d.Price)
                .IsRequired()
                .HasColumnType("decimal(18,4)");
        }
    }
}
