using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class VerifyEmailCommand : IRequest
    {
        public required Guid UserId { get; set; }
    }
}
