using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Data.Dto
{
    public abstract class DishForManipulationDto
    {
        /// <summary>
        /// The name of the dish
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of the dish
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Price of the dish
        /// </summary>
        public decimal Price { get; set; }
    }
}
