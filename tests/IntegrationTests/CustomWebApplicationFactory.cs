using System.Linq;
using Api;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly string _databaseName;

        public CustomWebApplicationFactory(string databaseName)
        {
            _databaseName = databaseName;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //replace database for each test class
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<RestaurantDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<RestaurantDbContext>(options => { options.UseInMemoryDatabase(_databaseName); });
            });
        }
    }
}