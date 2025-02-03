using FluentValidation;
using Ykotika.Application.Validation;

namespace Ykotika.Application.Commands
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(CategoryRules categoryRules)
        {
            categoryRules.Name(RuleFor(c => c.Name)).When(c => c.Name != null);
            categoryRules.Description(RuleFor(c => c.Description)).When(c => c.Description != null);
        }
    }
}
