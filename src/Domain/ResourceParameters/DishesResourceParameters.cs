namespace Domain.ResourceParameters
{
    /// <summary>
    ///     Dishes resource parameters with SortBy, SortDirection, SearchQuery, PageNumber, Name and MaximumPrice fields
    /// </summary>
    public class DishesResourceParameters : ResourceParameters
    {
        /// <summary>
        /// The name of the dish
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Maximum price of the dish
        /// </summary>
        public decimal? MaximumPrice { get; set; }
    }
}