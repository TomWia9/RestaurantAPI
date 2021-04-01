using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using RestaurantAPI.Data.Requests;

namespace RestaurantAPI.Shared.Validators
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
