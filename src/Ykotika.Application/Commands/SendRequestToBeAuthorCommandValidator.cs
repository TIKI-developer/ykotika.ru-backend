using FluentValidation;
using Ykotika.Application.Validation;

namespace Ykotika.Application.Commands
{
    public class SendRequestToBeAuthorCommandValidator : AbstractValidator<SendRequestToBeAuthorCommand>
    {
        public SendRequestToBeAuthorCommandValidator(UserRules userRules, AuthorRequestRules authorRequestRules)
        {
            userRules.Name(RuleFor(c => c.Name)).When(c => c.Name != null);
            userRules.Surname(RuleFor(c => c.Surname)).When(c => c.Surname != null);
            userRules.PhoneNumber(RuleFor(c => c.PhoneNumber)).When(c => c.PhoneNumber != null);
            authorRequestRules.AboutYourself(RuleFor(c => c.TellAboutYourself)).When(c => c.TellAboutYourself != null);
        }
    }
}
