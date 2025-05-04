using Ykotika.Application.Interfaces;
using Ykotika.Application.Models;

namespace Ykotika.NotificationSystem
{
    public class NotificationService : INotificationService, INotificationSender
    {
        public event EventHandler<NotifyDto>? NotificationReceived;

        public Task Send(NotifyDto dto)
        {
            NotificationReceived?.Invoke(this, dto);

            return Task.CompletedTask;
        }
    }
}
