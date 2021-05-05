using FluentValidation;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Shared.Validators
{
    public abstract class RestaurantValidator<T> : AbstractValidator<T> where T : RestaurantForManipulationDto
    {
        protected RestaurantValidator()
        {
            RuleFor(r => r.Name).NotEmpty().Length(2, 30);
            RuleFor(r => r.Description).Length(2, 500);
            RuleFor(r => r.Category).NotEmpty().Length(2, 30);
            RuleFor(r => r.HasDelivery).NotNull();
            RuleFor(r => r.ContactEmail).NotEmpty().Length(4, 40).EmailAddress();
            RuleFor(r => r.ContactNumber).NotEmpty().Length(7, 16).PhoneNumber();

            RuleFor(r => r.Address.City).NotEmpty().Length(2, 30);
            RuleFor(r => r.Address.Street).Length(2, 50);
            RuleFor(r => r.Address.PostalCode).NotEmpty().Length(5, 7).PostCode();
        }
    }
}
