using FluentValidation;
using Ykotika.Application.Validation;

namespace Ykotika.Application.Commands
{
    public class SignupCommandValidator : AbstractValidator<SignupCommand>
    {
        public SignupCommandValidator(AuthRules authRules, UserRules userRules)
        {
            userRules.Name(RuleFor(c => c.Name)).When(c => c.Name != null);
            authRules.Email(RuleFor(c => c.Email)).When(c => c.Email != null);
            authRules.Password(RuleFor(c => c.Password)).When(c => c.Password != null);
        }
    }
}
