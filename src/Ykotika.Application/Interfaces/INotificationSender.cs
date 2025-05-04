using Ykotika.Application.Models;

namespace Ykotika.Application.Interfaces
{
    public interface INotificationSender
    {
        event EventHandler<NotifyDto> NotificationReceived;
    }
}
