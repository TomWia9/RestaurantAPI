using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using RestaurantAPI.Data.Dto;
using Xunit;

namespace RestaurantAPI.IntegrationTests
{
    public class RestaurantsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetRestaurants_Should_ReturnListOfRestaurants()
        {
            //Arrange
            await AuthenticateAsync();

            //Act
            var response = await TestClient.GetAsync("api/restaurants");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadFromJsonAsync<IEnumerable<RestaurantDto>>()).Should().NotBeEmpty();
        }

        [Fact]
        public async Task PostRestaurant_Should_CreateNewRestaurant()
        {
            //Arrange
            await AuthenticateAsync();

            var newRestaurant = new RestaurantForCreationDto()
            {
                Name = "Test restaurant",
                Description = "Test description",
                Category = "Test category",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = new AddressDto()
                {
                    City = "Test city",
                    PostalCode = "12-345",
                    Street = "Test street"
                }
            };

            //Act
            var response = await TestClient.PostAsJsonAsync("api/restaurants", newRestaurant);

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
    }
}
