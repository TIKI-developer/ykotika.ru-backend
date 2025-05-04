namespace Ykotika.Domain.Entities
{
    public class Notification : Entity
    {
        public required string Title { get; set; }
        public required string Body { get; set; }
        public required bool IsRead { get; set; }
        public required string Type { get; set; }
        public required Dictionary<string, string> Metadata { get; set; } = new();
        public required Guid UserId { get; set; }
        public required User User { get; set; }
    }
}
