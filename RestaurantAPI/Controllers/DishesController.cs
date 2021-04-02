using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Models;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Controllers
{
    [Authorize]
    [Route("api/Restaurants/{restaurantId}/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;

        public DishesController(IDishesRepository dishesRepository, IMapper mapper)
        {
            _dishesRepository = dishesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetDishes(int restaurantId, [FromQuery] DishesResourceParameters dishesResourceParameters)
        {
            var dishes = await _dishesRepository.GetAllAsync(restaurantId, dishesResourceParameters);

            return Ok(_mapper.Map<IEnumerable<DishDto>>(dishes));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DishDto>> GetDish(int restaurantId, int id)
        {
            var dish = await _dishesRepository.GetAsync(restaurantId, id);

            if (dish == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DishDto>(dish));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<DishDto>> NewDish(int restaurantId, DishForCreationDto dish)
        {
            var newDish = _mapper.Map<Dish>(dish);
            newDish.RestaurantId = restaurantId;

            await _dishesRepository.AddAsync(newDish);

            if (await _dishesRepository.SaveChangesAsync())
            {
                return CreatedAtAction("GetDish", new {restaurantId, id = newDish.Id}, _mapper.Map<DishDto>(newDish));
            }

            return BadRequest();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDish(int restaurantId, int id, DishForUpdateDto dish)
        {
            var dishFromRepo = await _dishesRepository.GetAsync(restaurantId, id);

            if (dishFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(dish, dishFromRepo);

            _dishesRepository.Update(dishFromRepo);

            if (await _dishesRepository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest();

        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId, int id)
        {
            var dish = await _dishesRepository.GetAsync(restaurantId, id);

            if (dish == null)
            {
                return NotFound();
            }

            _dishesRepository.Delete(dish);

            if (await _dishesRepository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest();

        }
    }
}
