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
    public class DishesControllerTests : IntegrationTest
    {
        private readonly Guid _restaurantId;

        public DishesControllerTests() : base("DishesTests", true)
        {
            _restaurantId = Guid.Parse("8248d356-75f3-4cf6-9356-40dea7cd7a3d"); //id of one of seeded restaurant
        }

        [Fact]
        public async Task GetDishes_Should_BeStatusCode200()
        {
            //Act
            var response = await _client.GetAsync($"api/restaurants/{_restaurantId}/dishes");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetDishes_Should_Return2Dishes()
        {
            //Act
            var response = await _client.GetAsync($"api/restaurants/{_restaurantId}/dishes");

            //Assert
            var returnedDishes = await response.Content.ReadFromJsonAsync<IEnumerable<DishDto>>();
            returnedDishes.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetDish_Should_ReturnDishDto()
        {
            //Arrange
            var dishId = Guid.Parse("16fc7797-d79f-4bc5-a4ec-4e0950914618"); //id of one of seeded dish

            //Act
            var response = await _client.GetAsync($"api/restaurants/{_restaurantId}/dishes/{dishId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedRestaurant = await response.Content.ReadAsAsync<DishDto>();
            returnedRestaurant.Should().BeOfType<DishDto>();
        }

        [Fact]
        public async void GetDish_WithWrongId_Should_Return404()
        {
            //Arrange
            var dishId = Guid.Parse("55b3b4a4-4671-454b-aeec-b16835fb747c"); //wrong id

            //Act
            var response = await _client.GetAsync($"api/restaurants/{_restaurantId}/{dishId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PostDish_Should_CreateNewDish()
        {
            //Arrange
            await AuthHelper.AuthenticateAdminAsync(_client);
            var newDish = new DishForCreationDto()
            {
                Name = "Test dish",
                Description = "Test description",
                Price = 21.99M
            };

            //Act
            var response = await _client.PostAsJsonAsync($"api/restaurants/{_restaurantId}/dishes", newDish);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var returnedDish = await response.Content.ReadAsAsync<DishDto>();

            returnedDish.Name.Should().Be(newDish.Name);
            returnedDish.Description.Should().Be(newDish.Description);
            returnedDish.Price.Should().Be(newDish.Price);
        }

        [Fact]
        public async Task PostDish_WithoutAuth_Should_Return401()
        {
            //Arrange
            _client.DefaultRequestHeaders.Authorization = null;
            var newDish = new DishForCreationDto()
            {
                Name = "Test dish",
                Description = "Test description",
                Price = 21.99M
            };

            //Act
            var response = await _client.PostAsJsonAsync($"api/restaurants/{_restaurantId}/dishes", newDish);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PostDish_WithWrongAuth_Should_Return403() //role == user instead of administrator
        {
            //Arrange
            var newDish = new DishForCreationDto()
            {
                Name = "Test dish",
                Description = "Test description",
                Price = 21.99M
            };

            //Act
            var response = await _client.PostAsJsonAsync($"api/restaurants/{_restaurantId}/dishes", newDish);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task PutDish_Should_UpdateDish()
        {
            //Arrange
            await AuthHelper.AuthenticateAdminAsync(_client);
            var dishId = Guid.Parse("16fc7797-d79f-4bc5-a4ec-4e0950914618"); //id of one of seeded dish
            var updatedDish = new DishForUpdateDto()
            {
                Name = "Updated dish",
                Description = "Updated description",
                Price = 29.99M
            };

            //Act
            var response = await _client.PutAsJsonAsync($"api/restaurants/{_restaurantId}/dishes/{dishId}", updatedDish);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PutDish_WithoutAuth_Should_Return401()
        {
            //Arrange
            _client.DefaultRequestHeaders.Authorization = null;
            var dishId = Guid.Parse("16fc7797-d79f-4bc5-a4ec-4e0950914618"); //id of one of seeded dish
            var updatedDish = new DishForUpdateDto()
            {
                Name = "Updated dish",
                Description = "Updated description",
                Price = 29.99M
            };

            //Act
            var response = await _client.PutAsJsonAsync($"api/restaurants/{_restaurantId}/dishes/{dishId}", updatedDish);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task PutDish_WithWrongAuth_Should_Return403() //role == user instead of administrator
        {
            //Arrange
            var dishId = Guid.Parse("16fc7797-d79f-4bc5-a4ec-4e0950914618"); //id of one of seeded dish
            var updatedDish = new DishForUpdateDto()
            {
                Name = "Updated dish",
                Description = "Updated description",
                Price = 29.99M
            };

            //Act
            var response = await _client.PutAsJsonAsync($"api/restaurants/{_restaurantId}/dishes/{dishId}", updatedDish);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task DeleteDish_Should_Return204()
        {
            //Arrange
            await AuthHelper.AuthenticateAdminAsync(_client);
            var dishId = Guid.Parse("16fc7797-d79f-4bc5-a4ec-4e0950914618"); //id of one of seeded dish

            //Act
            var response = await _client.DeleteAsync($"api/restaurants/{_restaurantId}/dishes/{dishId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteDish_WithoutAuth_Should_Return401()
        {
            //Arrange
            _client.DefaultRequestHeaders.Authorization = null;
            var dishId = Guid.Parse("16fc7797-d79f-4bc5-a4ec-4e0950914618"); //id of one of seeded dish

            //Act
            var response = await _client.DeleteAsync($"api/restaurants/{_restaurantId}/dishes/{dishId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task DeleteDish_WithWrongAuth_Should_Return403() //role == user instead of administrator
        {
            //Arrange
            var dishId = Guid.Parse("16fc7797-d79f-4bc5-a4ec-4e0950914618"); //id of one of seeded dish

            //Act
            var response = await _client.DeleteAsync($"api/restaurants/{_restaurantId}/dishes/{dishId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
