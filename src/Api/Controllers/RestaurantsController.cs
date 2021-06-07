using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Restaurants.Commands.CreateRestaurant;
using Application.Restaurants.Commands.DeleteRestaurant;
using Application.Restaurants.Commands.UpdateRestaurant;
using Application.Restaurants.Queries.GetRestaurant;
using Application.Restaurants.Queries.GetRestaurants;
using Domain.Dto;
using Domain.ResourceParameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get a list of restaurants
        /// </summary>
        /// <param name="restaurantsResourceParameters">Query parameters to apply</param>
        /// <returns>An ActionResult of type IEnumerable of RestaurantDto</returns>
        /// <response code="200">Returns the list of restaurants</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants(
            [FromQuery] RestaurantsResourceParameters restaurantsResourceParameters)
        {
            var result = await _mediator.Send(new GetAllRestaurantsQuery(restaurantsResourceParameters));

            var metadata = new
            {
                result.TotalCount,
                result.PageSize,
                result.CurrentPage,
                result.TotalPages,
                result.HasNext,
                result.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(result);
        }

        /// <summary>
        ///     Get restaurant by id
        /// </summary>
        /// <param name="id">The Id of restaurant you want to get</param>
        /// <returns>An ActionResult of type RestaurantDto</returns>
        /// <response code="200">Returns the requested restaurant</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetRestaurant(Guid id)
        {
            var result = await _mediator.Send(new GetRestaurantQuery(id));
            return Ok(result);
        }

        /// <summary>
        ///     Update restaurant
        /// </summary>
        /// <param name="id">The Id of restaurant you want to update</param>
        /// <param name="restaurant">The restaurant with updated values</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(Guid id, RestaurantForUpdateDto restaurant)
        {
            await _mediator.Send(new UpdateRestaurantCommand(id, restaurant));
            return NoContent();
        }

        /// <summary>
        ///     Create new restaurant
        /// </summary>
        /// <param name="restaurant">The restaurant to create</param>
        /// <returns>An ActionResult of type RestaurantDto</returns>
        /// <response code="201">Creates and returns the created restaurant</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> PostRestaurant(RestaurantForCreationDto restaurant)
        {
            var result = await _mediator.Send(new CreateRestaurantCommand(restaurant));
            return CreatedAtAction("GetRestaurant", new {id = result.Id}, result);
        }

        /// <summary>
        ///     Delete the restaurant with given id
        /// </summary>
        /// <param name="id">The Id of restaurant you want to delete</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {
            await _mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();
        }
    }
}