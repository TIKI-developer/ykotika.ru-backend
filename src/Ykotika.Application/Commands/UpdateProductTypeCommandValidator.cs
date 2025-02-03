using FluentValidation;
using Ykotika.Application.Validation;

namespace Ykotika.Application.Commands
{
    public class UpdateProductTypeCommandValidator : AbstractValidator<UpdateProductTypeCommand>
    {
        public UpdateProductTypeCommandValidator(ProductTypeRules productTypeRules)
        {
            productTypeRules.Name(RuleFor(c => c.Name)).When(c => c.Name != null);
        }
    }
}
