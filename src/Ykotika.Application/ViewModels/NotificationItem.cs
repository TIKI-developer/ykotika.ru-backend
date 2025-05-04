using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class NotificationItem : IMapWith<Notification>
    {
        public required Guid Id { get; set; }
        public required Timestamps Timestamps { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
        public string? Href { get; set; }
        public required bool IsRead { get; set; }
        public required Guid UserId { get; set; }
    }
}
