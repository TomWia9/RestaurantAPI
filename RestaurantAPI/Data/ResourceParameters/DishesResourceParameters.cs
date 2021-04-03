using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Data.ResourceParameters
{
    public class DishesResourceParameters : ResourceParameters
    {
        public decimal? MaximumPrice { get; set; }
    }
}
