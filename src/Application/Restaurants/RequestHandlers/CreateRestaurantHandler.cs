using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Restaurants.Commands.CreateRestaurant;
using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using MediatR;

namespace Application.Restaurants.RequestHandlers
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
