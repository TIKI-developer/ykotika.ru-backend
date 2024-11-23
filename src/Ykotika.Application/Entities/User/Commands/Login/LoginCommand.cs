using MediatR;

namespace Ykotika.Application.Entities.User.Commands.Login
{
    public class LoginCommand : IRequest<LoginViewModel>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
