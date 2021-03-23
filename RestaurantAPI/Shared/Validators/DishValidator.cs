using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Shared.Validators
{
    public abstract class DishValidator<T> : AbstractValidator<T> where T: DishForManipulationDto
    {
        protected DishValidator()
        {
            RuleFor(d => d.Name).NotEmpty().Length(2, 30);
            RuleFor(d => d.Description).NotEmpty().Length(5, 500);
            RuleFor(d => d.Price).NotEmpty();

        }
    }
}
