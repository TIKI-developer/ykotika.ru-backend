using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateNotificationReadStatusCommand : IRequest
    {
        public required Guid UserId { get; set; }
        public required Guid Id { get; set; }
        public required bool IsRead { get; set; }
    }
}
