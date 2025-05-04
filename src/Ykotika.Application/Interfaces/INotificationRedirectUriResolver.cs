using Ykotika.Domain.Entities;

namespace Ykotika.Application.Interfaces
{
    public interface INotificationRedirectUriResolver
    {
        public string? ResolvedRedirectionUri(Notification notification); 
    }
}
