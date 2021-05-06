using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Dto;
using MediatR;

namespace Application.Dishes.Queries.GetDishes
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

            return new PagedList<DishDto>(_mapper.Map<IEnumerable<DishDto>>(dishes), dishes.TotalCount,
                request.DishesResourceParameters.PageNumber, request.DishesResourceParameters.PageSize);
        }
    }
}