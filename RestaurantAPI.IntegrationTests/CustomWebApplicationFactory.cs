using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Data.Requests;
using RestaurantAPI.Data.Response;
using RestaurantAPI.Models;

namespace RestaurantAPI.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly string _connectionString;

        public CustomWebApplicationFactory(string connectionString)
        {
            _connectionString = connectionString;
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

                services.AddDbContext<RestaurantDbContext>(options =>
                {
                    options.UseSqlServer(_connectionString);
                });
            });
        }
    }
}
