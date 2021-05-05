using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Dto;
using MediatR;

namespace Application.Dishes.Queries.GetDish
{
    public class GetDishHandler : IRequestHandler<GetDishQuery, DishDto>
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;

        public GetDishHandler(IDishesRepository dishesRepository, IMapper mapper)
        {
            _dishesRepository = dishesRepository;
            _mapper = mapper;
        }

        public async Task<DishDto> Handle(GetDishQuery request, CancellationToken cancellationToken)
        {
            if (!await _dishesRepository.RestaurantExists(request.RestaurantId))
            {
                throw new NotFoundException("Restaurant not found");
            }

            var dish = await _dishesRepository.GetAsync(request.RestaurantId, request.DishId);

            if (dish == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<DishDto>(dish);
        }
    }
}
