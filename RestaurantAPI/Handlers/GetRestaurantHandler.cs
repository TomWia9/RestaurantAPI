﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Queries;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Handlers
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

            return restaurant == null ? null : _mapper.Map<RestaurantDto>(restaurant);
        }
    }
}
