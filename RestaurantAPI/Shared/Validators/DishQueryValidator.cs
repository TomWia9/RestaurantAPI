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
    public class DishQueryValidator : QueryValidator<DishesResourceParameters>
    {
        private readonly string[] allowedSortByColumnNames =
        {
            nameof(Dish.Name),
            nameof(Dish.Description),
        };

        public DishQueryValidator()
        {
            RuleFor(d => d.MaximumPrice).GreaterThan(1);

            RuleFor(d => d.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value.FirstCharToUpper()))
                .WithMessage($"SortBy is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}
