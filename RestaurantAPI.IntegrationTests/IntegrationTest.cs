using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAPI.Models;
using Xunit;

namespace RestaurantAPI.IntegrationTests
{
    public abstract class IntegrationTest
    {
        protected readonly HttpClient _client;

        protected IntegrationTest(string connectionString)
        {
            var factory = new CustomWebApplicationFactory(connectionString);

            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var serviceProvider = services.BuildServiceProvider();

                    using var serviceScope = serviceProvider.CreateScope();
                    var context = serviceScope.ServiceProvider.GetService<RestaurantDbContext>();
                    var seeder = serviceScope.ServiceProvider.GetService<RestaurantSeeder>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    seeder.Seed();
                });

            }).CreateClient();
        }

    }
}
