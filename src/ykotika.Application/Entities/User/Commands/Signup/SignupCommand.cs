using MediatR;

namespace Ykotika.Application.Entities.User.Commands.Signup
{
    public class SignupCommand : IRequest<string>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
