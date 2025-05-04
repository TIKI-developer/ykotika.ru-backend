using Microsoft.Extensions.Options;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.NotificationSystem
{
    public class NotificationRedirectUriResolver(IOptions<NotificationSystemOptions> notificationSystemOptions) : INotificationRedirectUriResolver
    {
        private readonly List<RouteTemplate> _templates = notificationSystemOptions.Value.NotificationRouteTemplates;

        public string? ResolvedRedirectionUri(Notification notification)
        {
            foreach (var template in _templates)
            {
                if (!string.Equals(template.Type, notification.Type, StringComparison.OrdinalIgnoreCase))
                    continue;

                if (template.Conditions.All(c =>
                    notification.Metadata.TryGetValue(c.Key, out var value) &&
                    string.Equals(value, c.Value, StringComparison.OrdinalIgnoreCase)))
                {
                    var result = template.Template;
                    foreach (var kvp in notification.Metadata)
                    {
                        result = result.Replace($"{{{kvp.Key}}}", kvp.Value);
                    }

                    return result;
                }
            }

            return null;
        }
    }
}
