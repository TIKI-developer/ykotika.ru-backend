using FluentValidation;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Validation
{
    public class ProductRules : ValidationRules
    {
        public IRuleBuilderOptions<T, string?> Name<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(300);
        }
        public IRuleBuilderOptions<T, string?> Description<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty().WithMessage("Description is required.")
                    .MaximumLength(1000);
        }
        public IRuleBuilderOptions<T, List<Tag>?> Tags<T>(IRuleBuilder<T, List<Tag>?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .Must(items => items == null || items.Count <= 100)
                .WithMessage("Max count is 100");
        }
        public IRuleBuilderOptions<T, string?> TagValue<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("Max length is 255");
        }
    }
}
