using System;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class RestaurantDbContext : IdentityDbContext<User, Role, Guid>
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RestaurantsConfiguration());
            modelBuilder.ApplyConfiguration(new DishesConfiguration());
            modelBuilder.ApplyConfiguration(new AddressesConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
        }
    }
}
