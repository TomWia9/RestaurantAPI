using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Restaurants.Queries.GetRestaurants;
using AutoMapper;
using Domain.Dto;
using MediatR;

namespace Application.Restaurants.RequestHandlers
{
    public class GetAllRestaurantsHandler : IRequestHandler<GetAllRestaurantsQuery, PagedList<RestaurantDto>>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;

        public GetAllRestaurantsHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _restaurantsRepository.GetAllAsync(request.RestaurantsResourceParameters);

            return new PagedList<RestaurantDto>(_mapper.Map<IEnumerable<RestaurantDto>>(restaurants), restaurants.TotalCount, request.RestaurantsResourceParameters.PageNumber, request.RestaurantsResourceParameters.PageSize);
        }
    }
}
