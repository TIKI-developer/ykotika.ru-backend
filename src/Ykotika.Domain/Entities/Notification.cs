namespace Ykotika.Domain.Entities
{
    public class Notification : Entity
    {
        public required string Title { get; set; }
        public required string Body { get; set; }
        public string? Href { get; set; }
        public required bool IsRead { get; set; }
        public required Guid UserId { get; set; }
        public required User User { get; set; }
    }
}
