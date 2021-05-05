namespace Domain.ResourceParameters
{
    /// <summary>
    /// Restaurants resource parameters with SortBy, SortDirection, SearchQuery, PageNumber and HasDelivery fields
    /// </summary>
    public class RestaurantsResourceParameters : ResourceParameters
    {
        /// <summary>
        /// Restaurant having delivery (true or false)
        /// </summary>
        public bool? HasDelivery { get; set; }
    }
}
