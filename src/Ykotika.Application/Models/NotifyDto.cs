using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Models
{
    public record NotifyDto(Guid UserId, NotificationItem Notification);
}
