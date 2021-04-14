using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Commands
{
    public class CreateRestaurantCommand : IRequest<RestaurantDto>
    {
        public RestaurantForCreationDto RestaurantForCreation { get; set; }

        public CreateRestaurantCommand(RestaurantForCreationDto restaurant)
        {
            RestaurantForCreation = restaurant;
        }
    }
}
