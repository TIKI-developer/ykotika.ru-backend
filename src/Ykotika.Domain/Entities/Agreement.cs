using Ykotika.Domain.Interfaces;

namespace Ykotika.Domain.Entities
{
    public class Agreement : Entity, IPublishable, IHasAuthor
    {
        public required Offer Offer { get; set; }
        public required User User { get; set; }
        public required bool IsPublished { get; set; }
    }
}
