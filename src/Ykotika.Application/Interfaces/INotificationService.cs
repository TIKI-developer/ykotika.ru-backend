using Ykotika.Application.Models;

namespace Ykotika.Application.Interfaces
{
    public interface INotificationService
    {
        Task Send(NotifyDto dto);
    }
}
