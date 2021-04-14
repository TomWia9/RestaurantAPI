using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Commands;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Handlers
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

            _restaurantsRepository.Delete(restaurant);

            await _restaurantsRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
