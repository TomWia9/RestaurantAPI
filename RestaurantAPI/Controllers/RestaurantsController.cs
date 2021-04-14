﻿using System;
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
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Models;
using RestaurantAPI.Queries;
using RestaurantAPI.Repositories;
using RestaurantAPI.Shared.Events;

namespace RestaurantAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMediator _mediator;

        public RestaurantsController(IRestaurantsRepository restaurantsRepository, IMediator mediator)
        {
            _restaurantsRepository = restaurantsRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants([FromQuery] RestaurantsResourceParameters restaurantsResourceParameters)
        {
            var query = new GetAllRestaurantsQuery(restaurantsResourceParameters);
            var result = await _mediator.Send(query);

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
            var query = new GetRestaurantQuery(id);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, RestaurantForUpdateDto restaurant)
        {
            var result = await _mediator.Send(new UpdateRestaurantCommand(id, restaurant));

            return result switch
            {
                RestaurantNotFoundEvent => NotFound(),
                RestaurantUpdatedEvent => NoContent(),
                _ => BadRequest()
            };
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> PostRestaurant(RestaurantForCreationDto restaurant)
        {
            var result = await _mediator.Send(new CreateRestaurantCommand(restaurant));

            if (result != null)
            {
                return CreatedAtAction("GetRestaurant", new { id = result.Id }, result);
            }

            return BadRequest();

        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _restaurantsRepository.GetAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _restaurantsRepository.Delete(restaurant);

            if (await _restaurantsRepository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
