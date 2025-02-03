using FluentValidation;

namespace Ykotika.Application.Validation
{
    public class AuthorRequestRules : ValidationRules
    {
        public IRuleBuilderOptions<T, string?> AboutYourself<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty().WithMessage("About yourself is required.")
                    .MaximumLength(1000).WithMessage("About yourself max length is 1000");
        }
    }
}
