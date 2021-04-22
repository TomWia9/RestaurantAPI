using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using RestaurantAPI.Data.Requests;
using RestaurantAPI.Data.Response;
using Xunit;

namespace RestaurantAPI.IntegrationTests
{
    public class AuthControllerTests : IntegrationTest
    {
        public AuthControllerTests() : base("AuthTests", false)
        {
        }

        [Fact]
        public async Task SignUp_Should_RegisterNewUser()
        {
            //Arrange
            var userForCreation = new UserSignUpRequest
            {
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "Test",
                Password = "Qwerty123_",
                ConfirmPassword = "Qwerty123_"
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/auth/signup", userForCreation);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task SignUp_Should_ReturnToken()
        {
            //Arrange
            var userForCreation = new UserSignUpRequest
            {
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "Test",
                Password = "Qwerty123_",
                ConfirmPassword = "Qwerty123_"
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/auth/signup", userForCreation);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var result = await response.Content.ReadAsAsync<AuthenticationResponse>();
            result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task SignUp_WithExistingEmail_Should_ReturnBadRequest()
        {
            //Arrange
            var userForCreation = new UserSignUpRequest
            {
                Email = "admin@admin", //already existing email
                FirstName = "Test",
                LastName = "Test",
                Password = "Qwerty123_",
                ConfirmPassword = "Qwerty123_"
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/auth/signup", userForCreation);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
           
        }
        [Fact]
        public async Task SignUp_WithIncorrectPassword_Should_ReturnBadRequest()
        {
            //Arrange
            var userForCreation = new UserSignUpRequest
            {
                Email = "test@test.com", 
                FirstName = "Test",
                LastName = "Test",
                Password = "Q", //incorrect password
                ConfirmPassword = "Qwerty123_"
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/auth/signup", userForCreation);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
           
        }

        [Fact]
        public async Task SignIn_WithProperlyData_Should_ReturnToken()
        {
            //Arrange
            var userLoginRequest = new UserLoginRequest()
            {
                Email = "admin@admin", 
                Password = "Admin123_"
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/auth/signin", userLoginRequest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var authResult = await response.Content.ReadAsAsync<AuthenticationResponse>();
            authResult.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task SignIn_WithIncorrectData_Should_ReturnBadRequest()
        {
            //Arrange
            var userLoginRequest = new UserLoginRequest()
            {
                Email = "test", 
                Password = "test"
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/auth/signin", userLoginRequest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
