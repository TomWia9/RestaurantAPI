using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RestaurantAPI.Data.Requests;
using RestaurantAPI.Data.Response;

namespace RestaurantAPI.IntegrationTests.Helpers
{
    public static class AuthHelper
    {
        public static async Task AuthenticateAsync(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));
        }

        private static async Task<string> GetJwtAsync(HttpClient client)
        {
            var response = await client.PostAsJsonAsync("api/auth/signin", new UserLoginRequest()
            {
                Email = "admin@admin",
                Password = "Admin123_",

            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthenticationResponse>();

            return registrationResponse?.Token;
        }
    }
}
