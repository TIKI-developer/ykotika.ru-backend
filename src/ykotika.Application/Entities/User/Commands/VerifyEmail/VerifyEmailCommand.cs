using MediatR;

namespace Ykotika.Application.Entities.User.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest
    {
        public required Guid UserId { get; set; }
    }
}
