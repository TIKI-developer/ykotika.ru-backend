using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands.User
{
    public class VerifyEmailCommand : IRequest<LoginViewModel>
    {
        public required Guid UserId { get; set; }
    }
}
