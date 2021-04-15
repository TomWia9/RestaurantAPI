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
        public async Task GetRestaurants_ReturnsNotEmptyResponse()
        {
            //Arrange
            await AuthenticateAsync();

            //Act
            var response = await TestClient.GetAsync("api/restaurants");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadFromJsonAsync<IEnumerable<RestaurantDto>>()).Should().NotBeEmpty();
        }
    }
}
