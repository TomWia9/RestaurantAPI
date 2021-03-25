using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.EntityConfiguration;
using RestaurantAPI.Models.Auth;

namespace RestaurantAPI.Models
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
        }
    }
}
