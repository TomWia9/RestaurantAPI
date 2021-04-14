using MediatR;

namespace RestaurantAPI.Commands.Restaurants
{
    public class DeleteRestaurantCommand : IRequest
    {
        public int RestaurantId { get; set; }

        public DeleteRestaurantCommand(int id)
        {
            RestaurantId = id;
        }
    }
}
