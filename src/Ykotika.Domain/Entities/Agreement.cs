using Ykotika.Domain.Interfaces;

namespace Ykotika.Domain.Entities
{
    public class Agreement : Entity, IContent
    {
        public required Offer Offer { get; set; }
        public required bool IsPublished { get; set; }
        public required User Author { get; set; }
    }
}
