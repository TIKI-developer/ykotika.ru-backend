using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class Entity
    {
        public required Guid Id { get; set; }
        public required Timestamps Timestamps { get; set; }
    }
}
