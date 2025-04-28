namespace Ykotika.Domain.Entities
{
    public class Message : Entity
    {
        public string? Text { get; set; }
        public List<File>? Attachments { get; set; }
        public required User Sender { get; set; }
        public Guid ChatId { get; set; }
        public required Chat Chat { get; set; }
    }
}
