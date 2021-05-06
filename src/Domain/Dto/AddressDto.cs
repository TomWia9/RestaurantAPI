namespace Domain.Dto
{
    /// <summary>
    ///     The address of the restaurant with City, Street and PostalCode fields
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        ///     The restaurant city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     The restaurant street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        ///     The restaurant post code
        /// </summary>
        public string PostalCode { get; set; }
    }
}