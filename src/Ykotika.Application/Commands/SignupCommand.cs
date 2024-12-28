using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class SignupCommand : IRequest<Signup>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
