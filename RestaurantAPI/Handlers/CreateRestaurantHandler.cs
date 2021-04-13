using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RestaurantAPI.Commands;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Handlers
{
    public class CreateRestaurantHandler : IRequestHandler<CreateRestaurantCommand, RestaurantDto>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;

        public CreateRestaurantHandler(IMapper mapper, IRestaurantsRepository restaurantsRepository)
        {
            _mapper = mapper;
            _restaurantsRepository = restaurantsRepository;
        }

        public async Task<RestaurantDto> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var newRestaurant = _mapper.Map<Restaurant>(request);

            await _restaurantsRepository.AddAsync(newRestaurant);

            if (!await _restaurantsRepository.SaveChangesAsync())
            {
                return null;
            }

            return _mapper.Map<RestaurantDto>(newRestaurant);
        }
    }
}
