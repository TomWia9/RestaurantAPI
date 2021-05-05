using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Dishes.Commands.DeleteDish;
using MediatR;

namespace Application.Dishes.RequestHandlers
{
    public class DeleteDishHandler : IRequestHandler<DeleteDishCommand>
    {
        private readonly IDishesRepository _dishesRepository;

        public DeleteDishHandler(IDishesRepository dishesRepository)
        {
            _dishesRepository = dishesRepository;
        }

        public async Task<Unit> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
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

            await _dishesRepository.DeleteAsync(dish);

            return Unit.Value;
        }
    }
}
