using FluentValidation;

namespace Ykotika.Application.Validation
{
    public class CategoryRules : ValidationRules
    {
        public IRuleBuilderOptions<T, string?> Name<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(300).WithMessage("Name max length is 300");
        }
        public IRuleBuilderOptions<T, string?> Description<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty().WithMessage("Description is required.")
                    .MaximumLength(1000).WithMessage("Description max length is 1000");
        }
    }
}
