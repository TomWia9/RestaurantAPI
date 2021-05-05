using System;

namespace Domain.Entities
{
    public class Dish
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid RestaurantId { get; set; } 
        public virtual Restaurant Restaurant { get; set; }
    }
}
