using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands.User
{
    public class SignupCommand : IRequest<SignupViewModel>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
