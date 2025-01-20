using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class SignupCommand : IRequest<SignupResponse>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required bool ConfirmedPersonalDataProcessingPolicy { get; set; }
    }
}
