using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}
