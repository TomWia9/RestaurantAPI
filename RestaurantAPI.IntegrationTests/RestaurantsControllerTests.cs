using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.IntegrationTests.Helpers;
using RestaurantAPI.Models;
using Xunit;

namespace RestaurantAPI.IntegrationTests
{
    public class RestaurantsControllerTests : IClassFixture<CustomWebApplicationFactory>

    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        public RestaurantsControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        [Fact]
        public async Task GetRestaurants_Should_ReturnListOfRestaurants()
        {
            //Arrange
            await AuthHelper.AuthenticateAsync(_client);

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
            //Arrange
            await AuthHelper.AuthenticateAsync(_client);

            //Act
            var response = await _client.GetAsync("api/restaurants");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedRestaurants = await response.Content.ReadFromJsonAsync<IEnumerable<RestaurantDto>>();
            returnedRestaurants.Count().Should().Be(2);
        }

        [Fact]
        public async Task PostRestaurant_Should_CreateNewRestaurant()
        {
            //Arrange
            await AuthHelper.AuthenticateAsync(_client);

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
    }
}
