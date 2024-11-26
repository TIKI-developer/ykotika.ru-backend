using MediatR;
using Ykotika.Application.Entities.User.Commands.Login;

namespace Ykotika.Application.Entities.User.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<LoginViewModel>
    {
        public required Guid UserId { get; set; }
    }
}
