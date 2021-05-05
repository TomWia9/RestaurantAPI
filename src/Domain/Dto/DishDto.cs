using System;

namespace Domain.Dto
{
    /// <summary>
    /// The dish with Id, Name, Description, Price and RestaurantId fields
    /// </summary>
    public class DishDto
    {
        /// <summary>
        /// The Id of the dish
        /// </summary>
        public Guid Id { get; set; }
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
        /// <summary>
        /// The Id of restaurants which this dish belongs
        /// </summary>
        public Guid RestaurantId { get; set; }
    }
}
