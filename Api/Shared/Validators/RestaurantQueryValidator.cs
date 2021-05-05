using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Extensions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Shared.Validators
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
