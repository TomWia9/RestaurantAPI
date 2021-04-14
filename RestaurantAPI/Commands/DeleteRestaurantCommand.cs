using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RestaurantAPI.Commands
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
