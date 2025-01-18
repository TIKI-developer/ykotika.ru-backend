using Ykotika.Domain.Entities;

namespace Ykotika.Domain.ValueObjects
{
    public class Comment
    {
        public required User Author { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
