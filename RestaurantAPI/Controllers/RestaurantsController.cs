using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestaurantAPI.Commands;
using RestaurantAPI.Commands.Restaurants;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Models;
using RestaurantAPI.Queries;
using RestaurantAPI.Queries.Restaurants;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Controllers
{
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants([FromQuery] RestaurantsResourceParameters restaurantsResourceParameters)
        {
            var result = await _mediator.Send(new GetAllRestaurantsQuery(restaurantsResourceParameters));

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
        public async Task<ActionResult<RestaurantDto>> GetRestaurant(int id)
        {
            var result = await _mediator.Send(new GetRestaurantQuery(id));
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, RestaurantForUpdateDto restaurant)
        {
            await _mediator.Send(new UpdateRestaurantCommand(id, restaurant));
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> PostRestaurant(RestaurantForCreationDto restaurant)
        {
            var result = await _mediator.Send(new CreateRestaurantCommand(restaurant));
            return CreatedAtAction("GetRestaurant", new { id = result.Id }, result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            await _mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();
        }
    }
}
