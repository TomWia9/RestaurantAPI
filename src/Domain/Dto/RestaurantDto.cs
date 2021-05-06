using System;

namespace Domain.Dto
{
    /// <summary>
    ///     The restaurant with Id, Name, Description, Category, HasDelivery, ContactEmail, ContactNumber, and Address fields
    /// </summary>
    public class RestaurantDto
    {
        /// <summary>
        ///     The Id of the restaurant
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The description of the restaurant
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The category of the restaurant
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The category of the restaurant
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        ///     Restaurant having delivery (true or false)
        /// </summary>
        public bool HasDelivery { get; set; }

        /// <summary>
        ///     Email address of the restaurant
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        ///     Phone number of the restaurant
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        ///     The address of the restaurant
        /// </summary>
        public AddressDto Address { get; set; }
    }
}