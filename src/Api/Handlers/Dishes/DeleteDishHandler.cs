using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RestaurantAPI.Commands.Dishes;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Handlers.Dishes
{
    public class DeleteDishHandler : IRequestHandler<DeleteDishCommand>
    {
        private readonly IDishesRepository _dishesRepository;

        public DeleteDishHandler(IDishesRepository dishesRepository)
        {
            _dishesRepository = dishesRepository;
        }

        public async Task<Unit> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            if (!await _dishesRepository.RestaurantExists(request.RestaurantId))
            {
                throw new NotFoundException("Restaurant not found");
            }

            var dish = await _dishesRepository.GetAsync(request.RestaurantId, request.DishId);

            if (dish == null)
            {
                throw new NotFoundException();
            }

            await _dishesRepository.DeleteAsync(dish);

            return Unit.Value;
        }
    }
}
