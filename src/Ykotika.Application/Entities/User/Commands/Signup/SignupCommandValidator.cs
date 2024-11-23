using FluentValidation;
using Ykotika.Application.Validation;

namespace Ykotika.Application.Entities.User.Commands.Signup
{
    public class SignupCommandValidator : AbstractValidator<SignupCommand>
    {
        public SignupCommandValidator(AuthRules authRules)
        {
            authRules.Number(RuleFor(c => c.Email)).When(c => c.Email != null);
            authRules.Password(RuleFor(c => c.Password)).When(c => c.Password != null);
        }
    }
}
