using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Models;

namespace RestaurantAPI.Shared.Validators
{
    public abstract class QueryValidator<T> : AbstractValidator<T> where T: ResourceParameters
    {
        protected QueryValidator()
        {
            RuleFor(p => p.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(p => p.PageSize).GreaterThanOrEqualTo(1);
            RuleFor(p => p.SearchQuery).MaximumLength(100);
        }
    }
}
