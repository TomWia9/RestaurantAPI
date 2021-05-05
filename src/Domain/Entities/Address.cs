using System;

namespace Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
