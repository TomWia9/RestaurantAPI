using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RestaurantAPI.Commands.Restaurants;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Handlers.Restaurants
{
    public class UpdateRestaurantHandler : IRequestHandler<UpdateRestaurantCommand>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;

        public UpdateRestaurantHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurantFromRepo = await _restaurantsRepository.GetAsync(request.RestaurantId);

            if (restaurantFromRepo == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(request.RestaurantForUpdate, restaurantFromRepo);

            await _restaurantsRepository.UpdateAsync(restaurantFromRepo);

            return Unit.Value;
        }
    }
}
