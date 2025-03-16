using FluentValidation;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Validation
{
    public class AuthorRules : ValidationRules
    {
        public IRuleBuilderOptions<T, List<Social>?> Socials<T>(IRuleBuilder<T, List<Social>?> ruleBuilder)
        {
            return ruleBuilder
                .Must(list => list == null || (list.Count == 1))
                .WithMessage("Можно указать только одну соцсеть!");
        }
    }
}
