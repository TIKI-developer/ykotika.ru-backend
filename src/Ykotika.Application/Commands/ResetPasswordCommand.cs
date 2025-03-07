using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class ResetPasswordCommand : IRequest<LoginResponse>
    {
        public required string NewPassword { get; set; }
        public required string Token { get; set; }
    }
}
