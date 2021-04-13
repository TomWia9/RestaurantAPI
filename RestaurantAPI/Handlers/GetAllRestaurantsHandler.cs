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

namespace RestaurantAPI.Handlers
{
    public class GetAllRestaurantsHandler : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;

        public GetAllRestaurantsHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _restaurantsRepository.GetAllAsync(request.RestaurantsResourceParameters);

            //var metadata = new
            //{
            //    restaurants.TotalCount,
            //    restaurants.PagesSize,
            //    restaurants.CurrentPage,
            //    restaurants.TotalPages,
            //    restaurants.HasNext,
            //    restaurants.HasPrevious,
            //};

            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        }
    }
}
