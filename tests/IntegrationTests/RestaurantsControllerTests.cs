using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Domain.Dto;
using FluentAssertions;
using IntegrationTests.Helpers;
using Xunit;

namespace IntegrationTests
{
    public class RestaurantsControllerTests : IntegrationTest
    {
        public RestaurantsControllerTests() : base("RestaurantsTests", true)
        {
        }

        [Fact]
        public async Task GetRestaurants_Should_ReturnListOfRestaurants()
        {
            //Act
            var response = await _client.GetAsync("api/restaurants");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedRestaurants = await response.Content.ReadAsAsync<IEnumerable<RestaurantDto>>();
            returnedRestaurants.Should().AllBeOfType<RestaurantDto>();
        }

        [Fact]
        public async Task GetRestaurants_Should_Return2Restaurants()
        {
            //Act
            var response = await _client.GetAsync("api/restaurants");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedRestaurants = await response.Content.ReadFromJsonAsync<IEnumerable<RestaurantDto>>();
            returnedRestaurants.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetRestaurant_Should_ReturnRestaurantDto()
        {
            //Arrange
            var restaurantId = Guid.Parse("8248d356-75f3-4cf6-9356-40dea7cd7a3d"); //id of one of seeded restaurant

            //Act
            var response = await _client.GetAsync($"api/restaurants/{restaurantId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedRestaurant = await response.Content.ReadAsAsync<RestaurantDto>();
            returnedRestaurant.Should().BeOfType<RestaurantDto>();
        }

        [Fact]
        public async void GetRestaurant_WithWrongId_Should_Return404()
        {
            //Arrange
            var restaurantId = Guid.Parse("56aa8ab6-dc9e-4aa2-a128-ea15da8ff8ad"); //wrong id

            //Act
            var response = await _client.GetAsync($"api/restaurants/{restaurantId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PostRestaurant_Should_CreateNewRestaurant()
        {
            //Arrange
            await AuthHelper.AuthenticateAdminAsync(_client);
            var newRestaurant = new RestaurantForCreationDto
            {
                Name = "Test restaurant",
                Description = "Test description",
                Category = "Test category",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = new AddressDto
                {
                    City = "Test city",
                    PostalCode = "12-345",
                    Street = "Test street",
                    HouseNumber = "2"
                }
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/restaurants", newRestaurant);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var returnedRestaurant = await response.Content.ReadAsAsync<RestaurantDto>();

            returnedRestaurant.Name.Should().Be(newRestaurant.Name);
            returnedRestaurant.Description.Should().Be(newRestaurant.Description);
            returnedRestaurant.HasDelivery.Should().Be(newRestaurant.HasDelivery);
            returnedRestaurant.ContactEmail.Should().Be(newRestaurant.ContactEmail);
            returnedRestaurant.ContactNumber.Should().Be(newRestaurant.ContactNumber);
            returnedRestaurant.Address.City.Should().Be(newRestaurant.Address.City);
            returnedRestaurant.Address.Street.Should().Be(newRestaurant.Address.Street);
            returnedRestaurant.Address.PostalCode.Should().Be(newRestaurant.Address.PostalCode);
        }

        [Fact]
        public async Task PostRestaurant_WithoutAuth_Should_Return401()
        {
            //Arrange
            _client.DefaultRequestHeaders.Authorization = null;

            var newRestaurant = new RestaurantForUpdateDto
            {
                Name = "Test restaurant",
                Description = "Test description",
                Category = "Test category",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = new AddressDto
                {
                    City = "Test city",
                    PostalCode = "12-345",
                    Street = "Test street"
                }
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/restaurants", newRestaurant);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PostRestaurant_WithWrongAuth_Should_Return403() //role == user instead of administrator
        {
            //Arrange
            var newRestaurant = new RestaurantForUpdateDto
            {
                Name = "Test restaurant",
                Description = "Test description",
                Category = "Test category",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = new AddressDto
                {
                    City = "Test city",
                    PostalCode = "12-345",
                    Street = "Test street"
                }
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/restaurants", newRestaurant);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task PutRestaurant_Should_UpdateRestaurant()
        {
            //Arrange
            await AuthHelper.AuthenticateAdminAsync(_client);
            var restaurantId = Guid.Parse("8248d356-75f3-4cf6-9356-40dea7cd7a3d"); //id of one of seeded restaurant

            var updatedRestaurant = new RestaurantForCreationDto
            {
                Name = "Updated restaurant",
                Description = "Updated description",
                Category = "Updated category",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = new AddressDto
                {
                    City = "Updated city",
                    PostalCode = "12-345",
                    Street = "Updated street",
                    HouseNumber = "2"

                }
            };

            //Act
            var response = await _client.PutAsJsonAsync($"api/restaurants/{restaurantId}", updatedRestaurant);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PutRestaurant_WithWrongAuth_Should_Return403() //role == user instead of administrator
        {
            //Arrange
            var restaurantId = Guid.Parse("8248d356-75f3-4cf6-9356-40dea7cd7a3d"); //id of one of seeded restaurant
            var updatedRestaurant = new RestaurantForCreationDto
            {
                Name = "Updated restaurant",
                Description = "Updated description",
                Category = "Updated category",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = new AddressDto
                {
                    City = "Updated city",
                    PostalCode = "12-345",
                    Street = "Updated street"
                }
            };

            //Act
            var response = await _client.PutAsJsonAsync($"api/restaurants/{restaurantId}", updatedRestaurant);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task PutRestaurant_WithoutAuth_Should_Return401()
        {
            //Arrange
            _client.DefaultRequestHeaders.Authorization = null;
            var restaurantId = Guid.Parse("8248d356-75f3-4cf6-9356-40dea7cd7a3d"); //id of one of seeded restaurant
            var updatedRestaurant = new RestaurantForCreationDto
            {
                Name = "Updated restaurant",
                Description = "Updated description",
                Category = "Updated category",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = new AddressDto
                {
                    City = "Updated city",
                    PostalCode = "12-345",
                    Street = "Updated street"
                }
            };

            //Act
            var response = await _client.PutAsJsonAsync($"api/restaurants/{restaurantId}", updatedRestaurant);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task DeleteRestaurant_Should_Return204()
        {
            //Arrange
            await AuthHelper.AuthenticateAdminAsync(_client);
            var restaurantId = Guid.Parse("8248d356-75f3-4cf6-9356-40dea7cd7a3d"); //id of one of seeded restaurant

            //Act
            var response = await _client.DeleteAsync($"api/restaurants/{restaurantId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteRestaurant_WithoutAuth_Should_Return401()
        {
            //Arrange
            _client.DefaultRequestHeaders.Authorization = null;
            var restaurantId = Guid.Parse("8248d356-75f3-4cf6-9356-40dea7cd7a3d"); //id of one of seeded restaurant

            //Act
            var response = await _client.DeleteAsync($"api/restaurants/{restaurantId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task DeleteRestaurant_WithWrongAuth_Should_Return403() //role == user instead of administrator
        {
            //Arrange
            var restaurantId = Guid.Parse("8248d356-75f3-4cf6-9356-40dea7cd7a3d"); //id of one of seeded restaurant

            //Act
            var response = await _client.DeleteAsync($"api/restaurants/{restaurantId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}