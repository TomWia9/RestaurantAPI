using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RestaurantAPI.Commands.Dishes;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Handlers.Dishes
{
    public class CreateDishHandler : IRequestHandler<CreateDishCommand, DishDto>
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;

        public CreateDishHandler(IDishesRepository dishesRepository, IMapper mapper)
        {
            _dishesRepository = dishesRepository;
            _mapper = mapper;
        }

        public async Task<DishDto> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            if (!await _dishesRepository.RestaurantExists(request.RestaurantId))
            {
                throw new NotFoundException("Restaurant not found");
            }

            var newDish = _mapper.Map<Dish>(request.DishForCreation);
            newDish.RestaurantId = request.RestaurantId;

            await _dishesRepository.AddAsync(newDish);
            await _dishesRepository.SaveChangesAsync();

            return _mapper.Map<DishDto>(newDish);

        }
    }
}
