using FluentValidation;

namespace Ykotika.Application.Validation
{
    public class ProductTypeRules : ValidationRules
    {
        public IRuleBuilderOptions<T, string?> Name<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(300);
        }
    }
}
