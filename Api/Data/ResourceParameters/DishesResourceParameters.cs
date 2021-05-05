using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Data.ResourceParameters
{
    /// <summary>
    /// Dishes resource parameters with SortBy, SortDirection, SearchQuery, PageNumber and MaximumPrice fields
    /// </summary>
    public class DishesResourceParameters : ResourceParameters
    {
        /// <summary>
        /// Maximum price of the dish
        /// </summary>
        public decimal? MaximumPrice { get; set; }
    }
}
