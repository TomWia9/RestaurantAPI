using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Dish> Dishes { get; set; }
    }
}