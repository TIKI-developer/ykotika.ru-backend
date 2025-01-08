using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdatePasswordCommand : IRequest
    {
        public required Guid UserId { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
