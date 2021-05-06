using Domain.Enums;

namespace Domain.ResourceParameters
{
    public abstract class ResourceParameters
    {
        /// <summary>
        ///     Maximum page size
        /// </summary>
        private const int maxPageSize = 30;

        private int _pageSize = 10;

        /// <summary>
        ///     The field by you want to sort
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        ///     The direction by you want to sort
        /// </summary>
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;

        /// <summary>
        ///     Key words that you want to find
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        ///     Number of page
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        ///     Size of the page
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}