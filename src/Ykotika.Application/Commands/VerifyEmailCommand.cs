using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class VerifyEmailCommand : IRequest<LoginResponse>
    {
        public required Guid UserId { get; set; }
    }
}
