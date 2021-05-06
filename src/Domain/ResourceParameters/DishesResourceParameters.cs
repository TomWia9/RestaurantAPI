namespace Domain.ResourceParameters
{
    /// <summary>
    ///     Dishes resource parameters with SortBy, SortDirection, SearchQuery, PageNumber and MaximumPrice fields
    /// </summary>
    public class DishesResourceParameters : ResourceParameters
    {
        /// <summary>
        ///     Maximum price of the dish
        /// </summary>
        public decimal? MaximumPrice { get; set; }
    }
}