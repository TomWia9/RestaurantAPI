using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using MediatR;

namespace Application.Dishes.Commands.CreateDish
{
    public class CreateDishHandler : IRequestHandler<CreateDishCommand, DishDto>
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;

        public CreateDishHandler(IDishesRepository dishesRepository, IMapper mapper)
        {
            _dishesRepository = dishesRepository;
            _mapper = mapper;
        }

        public async Task<DishDto> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            if (!await _dishesRepository.RestaurantExists(request.RestaurantId))
            {
                throw new NotFoundException("Restaurant not found");
            }

            var newDish = _mapper.Map<Dish>(request.DishForCreation);
            newDish.RestaurantId = request.RestaurantId;

            await _dishesRepository.AddAsync(newDish);

            return _mapper.Map<DishDto>(newDish);

        }
    }
}
