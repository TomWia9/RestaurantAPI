using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Shared.Events;

namespace RestaurantAPI.Commands
{
    public class UpdateRestaurantCommand : RestaurantForManipulationDto, IRequest<IEvent>
    {
        public int RestaurantId { get; set; }
        public RestaurantForUpdateDto RestaurantForUpdate { get; set; }

        public UpdateRestaurantCommand(int id, RestaurantForUpdateDto restaurant)
        {
            RestaurantId = id;
            RestaurantForUpdate = restaurant;
        }
    }
}
