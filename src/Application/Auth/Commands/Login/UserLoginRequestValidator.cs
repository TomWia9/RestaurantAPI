using Domain.Requests;
using FluentValidation;

namespace Application.Auth.Commands.Login
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(u => u.Email).NotEmpty().MaximumLength(256).EmailAddress();
            RuleFor(u => u.Password).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(64);
        }
    }
}