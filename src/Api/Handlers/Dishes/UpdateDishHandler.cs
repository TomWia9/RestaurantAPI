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
    public class UpdateDishHandler : IRequestHandler<UpdateDishCommand>
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;

        public UpdateDishHandler(IDishesRepository dishesRepository, IMapper mapper)
        {
            _dishesRepository = dishesRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            if (!await _dishesRepository.RestaurantExists(request.RestaurantId))
            {
                throw new NotFoundException("Restaurant not found");
            }

            var dishFromRepo = await _dishesRepository.GetAsync(request.RestaurantId, request.DishId);

            if (dishFromRepo == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(request.DishForUpdate, dishFromRepo);

            await _dishesRepository.UpdateAsync(dishFromRepo);

            return Unit.Value;

        }
    }
}
