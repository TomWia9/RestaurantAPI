namespace Domain.ResourceParameters
{
    /// <summary>
    ///     Restaurants resource parameters with SortBy, SortDirection, SearchQuery, PageNumber, Name, City, Category and HasDelivery fields
    /// </summary>
    public class RestaurantsResourceParameters : ResourceParameters
    {
        /// <summary>
        ///     The name of the restaurant
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The City of the restaurant
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     The category of the restaurant
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        ///     Restaurant having delivery (true or false)
        /// </summary>
        public bool? HasDelivery { get; set; }
    }
}