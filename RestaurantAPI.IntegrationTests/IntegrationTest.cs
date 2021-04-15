using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.Requests;
using RestaurantAPI.Data.Response;
using RestaurantAPI.Models;

namespace RestaurantAPI.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(RestaurantDbContext));
                        services.AddDbContext<RestaurantDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });


            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("api/auth/signUp", new UserSignUpRequest()
            {
                Email = Guid.NewGuid() + "@test.com",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test123_",
                ConfirmPassword = "Test123_"

            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthenticationResponse>();

            return registrationResponse?.Token;
        }
    }
}
