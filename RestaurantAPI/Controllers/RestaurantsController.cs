using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantDbContext _context;
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;

        public RestaurantsController(RestaurantDbContext context, IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _context = context;
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _restaurantsRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<RestaurantDto>>(restaurants));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
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

            if(await _restaurantsRepository.UpdateAsync(restaurantFromRepo))
            {
                return NoContent();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");

        }


        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(RestaurantForCreationDto restaurant)
        {
            var newRestaurant = _mapper.Map<Restaurant>(restaurant);
            await _restaurantsRepository.AddAsync(newRestaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = newRestaurant.Id }, _mapper.Map<RestaurantDto>(newRestaurant));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _restaurantsRepository.GetAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            if (await _restaurantsRepository.DeleteAsync(restaurant))
            {
                return NoContent();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");

        }
    }
}
