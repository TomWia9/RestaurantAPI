using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Commands
{
    public class CreateRestaurantCommand : RestaurantForManipulationDto, IRequest<RestaurantDto>
    {
        
    }
}
