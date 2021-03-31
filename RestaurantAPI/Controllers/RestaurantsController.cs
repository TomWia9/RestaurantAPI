using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;

        public RestaurantsController(IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
            var restaurants = await _restaurantsRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<RestaurantDto>>(restaurants));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantsRepository.GetAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RestaurantDto>(restaurant));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, RestaurantForUpdateDto restaurant)
        {
            var restaurantFromRepo = await _restaurantsRepository.GetAsync(id);

            if (restaurantFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(restaurant, restaurantFromRepo);

            _restaurantsRepository.Update(restaurantFromRepo);

            if(await _restaurantsRepository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest();

        }


        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> PostRestaurant(RestaurantForCreationDto restaurant)
        {
            var newRestaurant = _mapper.Map<Restaurant>(restaurant);

            await _restaurantsRepository.AddAsync(newRestaurant);

            if (await _restaurantsRepository.SaveChangesAsync())
            {
                return CreatedAtAction("GetRestaurant", new { id = newRestaurant.Id }, _mapper.Map<RestaurantDto>(newRestaurant));
            }

            return BadRequest();

        }

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
