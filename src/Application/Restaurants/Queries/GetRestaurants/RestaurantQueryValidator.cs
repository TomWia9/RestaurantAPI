using System.Linq;
using Application.Common.Abstractions;
using Application.Common.Extensions;
using Domain.Entities;
using Domain.ResourceParameters;
using FluentValidation;

namespace Application.Restaurants.Queries.GetRestaurants
{
    public class RestaurantQueryValidator : QueryValidator<RestaurantsResourceParameters>
    {
        private readonly string[] allowedSortByColumnNames =
        {
            nameof(Restaurant.Name),
            nameof(Restaurant.Description),
            nameof(Restaurant.Category),
            nameof(Restaurant.Address.City),
        };

        public RestaurantQueryValidator()
        {
            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value.FirstCharToUpper()))
            .WithMessage($"SortBy is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}
