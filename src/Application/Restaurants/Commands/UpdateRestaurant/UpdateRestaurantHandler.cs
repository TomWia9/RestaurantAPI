using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Restaurants.Commands.UpdateRestaurant
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
