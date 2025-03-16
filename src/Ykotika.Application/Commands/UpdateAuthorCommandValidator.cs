using FluentValidation;
using Ykotika.Application.Validation;

namespace Ykotika.Application.Commands
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator(AuthorRules authorRules) 
        {
            authorRules.Socials(RuleFor(e => e.Socials)).When(e => e.Socials != null);
        }
    }
}
