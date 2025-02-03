using FluentValidation;
using Ykotika.Application.Validation;

namespace Ykotika.Application.Commands
{
    public class CreateProductTypeCommandValidator : AbstractValidator<CreateProductTypeCommand>
    {
        public CreateProductTypeCommandValidator(ProductTypeRules productTypeRules)
        {
            productTypeRules.Name(RuleFor(c => c.Name)).When(c => c.Name != null);
        }
    }
}
