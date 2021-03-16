using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAPI.Models;

namespace RestaurantAPI.EntityConfiguration
{
    public class RestaurantsConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(r => r.Description)
                .HasMaxLength(100);

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

            
        }
    }
}
