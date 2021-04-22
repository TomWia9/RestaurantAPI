using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAPI.IntegrationTests.Helpers;
using RestaurantAPI.Models;
using Xunit;

namespace RestaurantAPI.IntegrationTests
{
    public abstract class IntegrationTest
    {
        protected readonly HttpClient _client;

        protected IntegrationTest(string databaseName, bool authenticate)
        {
            var factory = new CustomWebApplicationFactory(databaseName);

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

            if (authenticate)
            {
                AuthHelper.AuthenticateAsync(_client).Wait();
            }
        }

    }
}
