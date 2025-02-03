using FluentValidation;

namespace Ykotika.Application.Validation
{
    public class UserRules : ValidationRules
    {
        public IRuleBuilderOptions<T, string?> Name<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(50).WithMessage("Max length is 50");
        }
        public IRuleBuilderOptions<T, string?> Surname<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty().WithMessage("Surname is required.")
                    .MaximumLength(50).WithMessage("Max length is 50");
        }
        public IRuleBuilderOptions<T, string?> PhoneNumber<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty().WithMessage("Phone number is required.")
                    .MaximumLength(20).WithMessage("Max length is 20");
        }
    }
}
