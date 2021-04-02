using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Data.ResourceParameters
{
    public class RestaurantsResourceParameters : ResourceParameters
    {
        public bool? HasDelivery { get; set; }
    }
}
