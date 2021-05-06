using System.Net.Http;
using Infrastructure.Persistence;
using IntegrationTests.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
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

            if (authenticate) AuthHelper.AuthenticateUserAsync(_client).Wait();
        }
    }
}