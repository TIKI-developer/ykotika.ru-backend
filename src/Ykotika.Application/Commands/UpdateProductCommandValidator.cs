using FluentValidation;
using Ykotika.Application.Validation;

namespace Ykotika.Application.Commands
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator(ProductRules productRules)
        {
            productRules.Name(RuleFor(c => c.Name)).When(c => c.Name != null);
            productRules.Description(RuleFor(c => c.Description)).When(c => c.Description != null);
            productRules.Tags(RuleFor(c => c.Tags)).When(c => c.Tags != null);
            RuleForEach(c => c.Tags)
            .ChildRules(tag =>
            {
                productRules.TagValue(tag.RuleFor(t => t.Value)).When(t => t.Value != null);
            });
        }
    }
}
