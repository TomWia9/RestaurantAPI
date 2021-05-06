using System.Linq;
using FluentValidation;

namespace Application.Common.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderInitial<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return (IRuleBuilderInitial<T, string>) ruleBuilder.Custom((phoneNumber, context) =>
            {
                if (phoneNumber.Length < 9) context.AddFailure("The phone number is to short");

                if (phoneNumber.Length > 16) context.AddFailure("The phone number is to long");

                phoneNumber = phoneNumber.Replace("+", "");
                phoneNumber = phoneNumber.Replace(" ", "");
                phoneNumber = phoneNumber.Replace("-", "");
                if (phoneNumber.All(char.IsDigit) == false)
                    context.AddFailure("The phone number contains invalid characters.");
            });
        }

        public static IRuleBuilderInitial<T, string> PostCode<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return (IRuleBuilderInitial<T, string>) ruleBuilder.Custom((postcode, context) =>
            {
                if (postcode == null) return;

                postcode = postcode.Replace(" ", "");
                postcode = postcode.Replace("-", "");

                if (postcode.All(char.IsDigit) == false) context.AddFailure("Post code contains invalid characters.");

                if (postcode.Length > 5) context.AddFailure("Post code is to long");

                if (postcode.Length < 5) context.AddFailure("Post code is to short");
            });
        }
    }
}