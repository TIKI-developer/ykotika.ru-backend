namespace Ykotika.Domain.Entities
{
    public class Offer : Entity
    {
        public required string Content { get; set; }
        public required bool IsPublished { get; set; }
    }
}
