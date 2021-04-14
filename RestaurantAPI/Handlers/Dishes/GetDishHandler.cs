using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Queries.Dishes;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Handlers.Dishes
{
    public class GetDishHandler : IRequestHandler<GetDishQuery, DishDto>
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;

        public GetDishHandler(IDishesRepository dishesRepository, IMapper mapper)
        {
            _dishesRepository = dishesRepository;
            _mapper = mapper;
        }

        public async Task<DishDto> Handle(GetDishQuery request, CancellationToken cancellationToken)
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

            return _mapper.Map<DishDto>(dish);
        }
    }
}
