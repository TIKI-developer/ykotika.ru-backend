using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateAllNotificationsReadStatusCommand : IRequest
    {
        public required Guid UserId { get; set; }
        public required bool IsRead { get; set; }
    }
}
