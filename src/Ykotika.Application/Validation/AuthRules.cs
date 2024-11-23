using FluentValidation;

namespace Ykotika.Application.Validation
{
    public class AuthRules : ValidationRules
    {
        private readonly string _passwordExpression = "^(?=.*[a-z])(?=.*\\d)[A-Za-z\\d]{8,50}$";

        public IRuleBuilderOptions<T, string?> Number<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Invalid email format.");
        }
        public IRuleBuilderOptions<T, string?> Password<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                   .MinimumLength(8)
                   .WithMessage("Пароль должен иметь больше 8 символов!")
                   .MaximumLength(50)
                   .WithMessage("Пароль должен иметь меньше 50 символов!")
                   .Matches(_passwordExpression)
                   .WithMessage("Пароль должен иметь хотя бы одну букву и одну цифру");
        }
    }
}
