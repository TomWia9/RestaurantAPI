using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Data.Dto
{
    public abstract class RestaurantForManipulationDto
    {
        /// <summary>
        /// The name of the restaurant
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of the restaurant
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The category of the restaurant
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Restaurant having delivery (true or false)
        /// </summary>
        public bool HasDelivery { get; set; }
        /// <summary>
        /// Email address of the restaurant
        /// </summary>
        public string ContactEmail { get; set; }
        /// <summary>
        /// Phone number of the restaurant
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// The address of the restaurant
        /// </summary>
        public AddressDto Address { get; set; }
    }
}
