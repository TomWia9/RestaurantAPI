using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using RestaurantAPI.Commands.Dishes;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Models;
using RestaurantAPI.Queries.Dishes;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Controllers
{
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetDishes(Guid restaurantId, [FromQuery] DishesResourceParameters dishesResourceParameters)
        {
            var result = await _mediator.Send(new GetAllDishesQuery(restaurantId, dishesResourceParameters));

            var metadata = new
            {
                result.TotalCount,
                result.PagesSize,
                result.CurrentPage,
                result.TotalPages,
                result.HasNext,
                result.HasPrevious,
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DishDto>> GetDish(Guid restaurantId, Guid id)
        {
            var result = await _mediator.Send(new GetDishQuery(restaurantId, id));
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<DishDto>> NewDish(Guid restaurantId, DishForCreationDto dish)
        {
            var result = await _mediator.Send(new CreateDishCommand(restaurantId, dish));
            return CreatedAtAction("GetDish", new {restaurantId, id = result.Id}, result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDish(Guid restaurantId, Guid id, DishForUpdateDto dish)
        {
            await _mediator.Send(new UpdateDishCommand(restaurantId, id, dish));
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(Guid restaurantId, Guid id)
        {
            await _mediator.Send(new DeleteDishCommand(restaurantId, id));
            return NoContent();
        }
    }
}
