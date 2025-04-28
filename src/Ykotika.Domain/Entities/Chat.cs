namespace Ykotika.Domain.Entities
{
    public class Chat : Entity
    {
        public string? Name { get; set; }
        public required List<User> Members { get; set; }
        public List<Message> Messages { get; set; } = [];
    }
}
