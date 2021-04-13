using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Queries;
using RestaurantAPI.Repositories;
using RestaurantAPI.Shared.PagedList;

namespace RestaurantAPI.Handlers
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
