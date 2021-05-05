using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RestaurantAPI.Commands.Restaurants;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Handlers.Restaurants
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
            var newRestaurant = _mapper.Map<Restaurant>(request.RestaurantForCreation);

            await _restaurantsRepository.AddAsync(newRestaurant);

            return _mapper.Map<RestaurantDto>(newRestaurant);
        }
    }
}
