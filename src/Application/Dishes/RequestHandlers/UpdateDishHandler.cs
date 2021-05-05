using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Dishes.Commands.UpdateDish;
using AutoMapper;
using MediatR;

namespace Application.Dishes.RequestHandlers
{
    public class UpdateDishHandler : IRequestHandler<UpdateDishCommand>
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;

        public UpdateDishHandler(IDishesRepository dishesRepository, IMapper mapper)
        {
            _dishesRepository = dishesRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            if (!await _dishesRepository.RestaurantExists(request.RestaurantId))
            {
                throw new NotFoundException("Restaurant not found");
            }

            var dishFromRepo = await _dishesRepository.GetAsync(request.RestaurantId, request.DishId);

            if (dishFromRepo == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(request.DishForUpdate, dishFromRepo);

            await _dishesRepository.UpdateAsync(dishFromRepo);

            return Unit.Value;

        }
    }
}
