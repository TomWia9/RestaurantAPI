using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dishes.Commands.CreateDish;
using Application.Dishes.Commands.DeleteDish;
using Application.Dishes.Commands.UpdateDish;
using Application.Dishes.Queries.GetDish;
using Application.Dishes.Queries.GetDishes;
using Domain.Dto;
using Domain.ResourceParameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    [Route("api/Restaurants/{restaurantId}/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get a list of dishes from specified restaurant
        /// </summary>
        /// <param name="restaurantId">The Id of restaurant you want to get dishes from</param>
        /// <param name="dishesResourceParameters">Query parameters to apply</param>
        /// <returns>An ActionResult of type IEnumerable of DishDto</returns>
        /// <response code="200">Returns the list of dishes</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetDishes(Guid restaurantId,
            [FromQuery] DishesResourceParameters dishesResourceParameters)
        {
            var result = await _mediator.Send(new GetAllDishesQuery(restaurantId, dishesResourceParameters));

            var metadata = new
            {
                result.TotalCount,
                result.PageSize,
                result.CurrentPage,
                result.TotalPages,
                result.HasNext,
                result.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            }));

            return Ok(result);
        }

        /// <summary>
        ///     Get single dish from specified restaurant
        /// </summary>
        /// <param name="restaurantId">The Id of restaurant you want to get dish from</param>
        /// <param name="id">The Id of dish you want to get</param>
        /// <returns>An ActionResult of type DishDto</returns>
        /// <response code="200">Returns the requested dish from specified restaurant</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<DishDto>> GetDish(Guid restaurantId, Guid id)
        {
            var result = await _mediator.Send(new GetDishQuery(restaurantId, id));
            return Ok(result);
        }

        /// <summary>
        ///     Create a new dish for restaurant
        /// </summary>
        /// <param name="restaurantId">The id of restaurant for which to create dish</param>
        /// <param name="dish">Dish to create</param>
        /// <returns>An ActionResult of type DishDto</returns>
        /// <response code="201">Creates and returns the created dish</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<DishDto>> NewDish(Guid restaurantId, DishForCreationDto dish)
        {
            var result = await _mediator.Send(new CreateDishCommand(restaurantId, dish));
            return CreatedAtAction("GetDish", new {restaurantId, id = result.Id}, result);
        }

        /// <summary>
        ///     Update dish
        /// </summary>
        /// <param name="restaurantId">The Id of restaurant where you want to update dish</param>
        /// <param name="id">The Id of dish you want to update</param>
        /// <param name="dish">Dish with updated values</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDish(Guid restaurantId, Guid id, DishForUpdateDto dish)
        {
            await _mediator.Send(new UpdateDishCommand(restaurantId, id, dish));
            return NoContent();
        }

        /// <summary>
        ///     Delete the dish with given id
        /// </summary>
        /// <param name="restaurantId">The Id of restaurant you want to delete dish from</param>
        /// <param name="id">The id of dish you want to delete</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(Guid restaurantId, Guid id)
        {
            await _mediator.Send(new DeleteDishCommand(restaurantId, id));
            return NoContent();
        }
    }
}