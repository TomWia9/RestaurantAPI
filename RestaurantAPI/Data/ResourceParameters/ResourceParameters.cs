using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantAPI.Shared.Enums;

namespace RestaurantAPI.Data.ResourceParameters
{
    public abstract class ResourceParameters
    {
        private const int maxPageSize = 30;
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
