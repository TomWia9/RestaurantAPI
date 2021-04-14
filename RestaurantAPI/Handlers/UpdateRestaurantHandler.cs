using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RestaurantAPI.Commands;
using RestaurantAPI.Repositories;
using RestaurantAPI.Shared.Events;

namespace RestaurantAPI.Handlers
{
    public class UpdateRestaurantHandler : IRequestHandler<UpdateRestaurantCommand, IEvent>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;

        public UpdateRestaurantHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
        }

        public async Task<IEvent> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurantFromRepo = await _restaurantsRepository.GetAsync(request.RestaurantId);

            if (restaurantFromRepo == null)
            {
                return new RestaurantNotFoundEvent();
            }

            _mapper.Map(request.RestaurantForUpdate, restaurantFromRepo);

            _restaurantsRepository.Update(restaurantFromRepo);

            if(await _restaurantsRepository.SaveChangesAsync())
            {
                return new RestaurantUpdatedEvent();
            }

            return null;
        }
    }
}
