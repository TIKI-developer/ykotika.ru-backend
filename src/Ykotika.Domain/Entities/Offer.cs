using Ykotika.Domain.Interfaces;

namespace Ykotika.Domain.Entities
{
    public class Offer : Entity, IContent
    {
        public required string Content { get; set; }
        public required bool IsPublished { get; set; }
        public required User User { get; set; }
        public List<Agreement>? Agreements { get; set; }
    }
}
