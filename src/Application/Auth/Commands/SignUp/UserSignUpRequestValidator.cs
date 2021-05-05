using Domain.Requests;
using FluentValidation;

namespace Application.Auth.Commands.SignUp
{
    public class UserSignUpRequestValidator : AbstractValidator<UserSignUpRequest>
    {
        public UserSignUpRequestValidator()
        {
            RuleFor(u => u.Email).NotEmpty().MaximumLength(256).EmailAddress();
            RuleFor(u => u.FirstName).NotEmpty().MaximumLength(30);
            RuleFor(u => u.LastName).NotEmpty().MaximumLength(30);
            RuleFor(u => u.Password).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(64);
            RuleFor(u => u.ConfirmPassword).NotEmpty().Equal(u => u.Password)
                .When(u => !string.IsNullOrWhiteSpace(u.Password)).WithMessage("Passwords do not match");

        }
    }
}
