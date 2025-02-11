using FluentValidation;
using Ykotika.Application.Validation;

namespace Ykotika.Application.Commands
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator(UserRules userRules)
        {
            userRules.Name(RuleFor(c => c.Name)).When(c => c.Name != null);
        }
    }
}
