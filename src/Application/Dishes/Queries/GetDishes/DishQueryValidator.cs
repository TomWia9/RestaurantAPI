using System.Linq;
using Application.Common.Abstractions;
using Application.Common.Extensions;
using Domain.Entities;
using Domain.ResourceParameters;
using FluentValidation;

namespace Application.Dishes.Queries.GetDishes
{
    public class DishQueryValidator : QueryValidator<DishesResourceParameters>
    {
        private readonly string[] allowedSortByColumnNames =
        {
            nameof(Dish.Name),
            nameof(Dish.Description)
        };

        public DishQueryValidator()
        {
            RuleFor(d => d.MaximumPrice).GreaterThan(1);

            RuleFor(d => d.SortBy)
                .Must(value =>
                    string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value.FirstCharToUpper()))
                .WithMessage($"SortBy is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}