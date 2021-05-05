using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Dto;
using MediatR;

namespace Application.Restaurants.Queries.GetRestaurant
{
    public class GetRestaurantHandler : IRequestHandler<GetRestaurantQuery, RestaurantDto>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;

        public GetRestaurantHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
        }

        public async Task<RestaurantDto> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantsRepository.GetAsync(request.RestaurantId);

            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<RestaurantDto>(restaurant);
        }
    }
}
