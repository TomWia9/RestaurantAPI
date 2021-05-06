using Domain.ResourceParameters;
using FluentValidation;

namespace Application.Common.Abstractions
{
    public abstract class QueryValidator<T> : AbstractValidator<T> where T : ResourceParameters
    {
        protected QueryValidator()
        {
            RuleFor(p => p.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(p => p.PageSize).GreaterThanOrEqualTo(1);
            RuleFor(p => p.SearchQuery).MaximumLength(100);
        }
    }
}