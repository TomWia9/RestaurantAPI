using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Requests;

namespace IntegrationTests.Helpers
{
    public static class AuthHelper
    {
        public static async Task AuthenticateAdminAsync(HttpClient client)
        {
            var loginRequest = new UserLoginRequest
            {
                Email = "admin@admin",
                Password = "Admin123_"
            };

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", await GetJwtAsync(client, loginRequest));
        }

        public static async Task AuthenticateUserAsync(HttpClient client)
        {
            var loginRequest = new UserLoginRequest
            {
                Email = "user@example.com",
                Password = "Qwerty123_"
            };

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", await GetJwtAsync(client, loginRequest));
        }

        private static async Task<string> GetJwtAsync(HttpClient client, UserLoginRequest loginRequest)
        {
            var response = await client.PostAsJsonAsync("api/auth/signin", loginRequest);

            var registrationResponse = await response.Content.ReadAsAsync<AuthenticationResponse>();

            return registrationResponse?.Token;
        }
    }
}