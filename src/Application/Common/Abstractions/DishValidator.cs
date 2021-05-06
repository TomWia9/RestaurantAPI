using Domain.Dto;
using FluentValidation;

namespace Application.Common.Abstractions
{
    public abstract class DishValidator<T> : AbstractValidator<T> where T : DishForManipulationDto
    {
        protected DishValidator()
        {
            RuleFor(d => d.Name).NotEmpty().Length(2, 30);
            RuleFor(d => d.Description).Length(5, 500);
            RuleFor(d => d.Price).NotEmpty().GreaterThan(0);
        }
    }
}