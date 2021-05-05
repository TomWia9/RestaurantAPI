using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Queries.Dishes;
using RestaurantAPI.Repositories;
using RestaurantAPI.Shared.PagedList;

namespace RestaurantAPI.Handlers.Dishes
{
    public class GetAllDishesHandler : IRequestHandler<GetAllDishesQuery, PagedList<DishDto>>
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;

        public GetAllDishesHandler(IDishesRepository dishesRepository, IMapper mapper)
        {
            _dishesRepository = dishesRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _dishesRepository.GetAllAsync(request.RestaurantId, request.DishesResourceParameters);

            return new PagedList<DishDto>(_mapper.Map<IEnumerable<DishDto>>(dishes), dishes.TotalCount, request.DishesResourceParameters.PageNumber, request.DishesResourceParameters.PageSize);
        }
    }
}
