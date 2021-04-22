using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Models.Auth;

namespace RestaurantAPI.Models
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RestaurantSeeder> _logger;

        public RestaurantSeeder(RestaurantDbContext context, RoleManager<Role> roleManager, UserManager<User> userManager, ILogger<RestaurantSeeder> logger)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public void Seed()
        {

            _logger.LogInformation("Connecting to database");

            if (!_context.Database.CanConnect()) return;

            _logger.LogInformation("Connected to database");

            if (_context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                var pendingMigrations = _context.Database.GetPendingMigrations();

                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _context.Database.Migrate();
                }
            }


            if (!_context.Restaurants.Any())
            {

                var restaurants = GetRestaurants();

                _context.Restaurants.AddRange(restaurants);
                _context.SaveChanges();
                _logger.LogInformation("Seeded restaurants");

            }

            if (!_roleManager.Roles.Any())
            {
                _roleManager.CreateAsync(new Role()
                {
                    Name = "User"
                }).Wait();

                _roleManager.CreateAsync(new Role()
                {
                    Name = "Administrator"
                }).Wait();

                _logger.LogInformation("Seeded roles");

            }

            if (_userManager.Users.Any()) return;

            var newUser = new User()
            {
                Email = "admin@admin",
                UserName = "admin@admin",
                FirstName = "admin",
                LastName = "admin"
            };

            _userManager.CreateAsync(newUser,"Admin123_").Wait();
            _userManager.AddToRoleAsync(newUser, "Administrator").Wait();

            _logger.LogInformation("Seeded user");
        }

        private static IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Id = Guid.Parse("8248d356-75f3-4cf6-9356-40dea7cd7a3d"),
                    Name = "KFC",
                    Category = "Fast Food",
                    Description =
                        "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville",
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "123456789",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Id = Guid.Parse("16fc7797-d79f-4bc5-a4ec-4e0950914618"),
                            Name = "Nashville Hot Chicken",
                            Price = 10.30M,
                        },

                        new Dish()
                        {
                            Id = Guid.Parse("773b1ffe-bf9d-4c3d-8204-e95a2b171d06"),
                            Name = "Chicken Nuggets",
                            Price = 5.30M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },
                new Restaurant()
                {
                    Id = Guid.Parse("62694f89-9691-4464-88b8-2dcdb12a2d53"),
                    Name = "McDonald Szewska",
                    Category = "Fast Food",
                    Description =
                        "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    ContactEmail = "contact@mcdonald.com",
                    ContactNumber = "987654321",
                    HasDelivery = true,
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Szewska 2",
                        PostalCode = "30-001"
                    }
                }
            };

            return restaurants;
        }
    }
}
