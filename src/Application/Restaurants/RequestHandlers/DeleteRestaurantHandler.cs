using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Restaurants.Commands.DeleteRestaurant;
using MediatR;

namespace Application.Restaurants.RequestHandlers
{
    public class DeleteRestaurantHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;

        public DeleteRestaurantHandler(IRestaurantsRepository restaurantsRepository)
        {
            _restaurantsRepository = restaurantsRepository;
        }

        public async Task<Unit> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantsRepository.GetAsync(request.RestaurantId);

            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            await _restaurantsRepository.DeleteAsync(restaurant);

            return Unit.Value;
        }
    }
}
