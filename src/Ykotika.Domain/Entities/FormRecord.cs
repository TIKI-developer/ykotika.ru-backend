using Ykotika.Domain.Interfaces;

namespace Ykotika.Domain.Entities
{
    public class FormRecord : Entity, IHasAuthor, IPublishable
    {
        public required Form Form { get; set; }
        public required List<InputRecord> InputRecords { get; set; } = [];
        public required User User { get; set; }
        public bool IsPublished { get; set; }

        public class InputRecord
        {
            public required string Id { get; set; }
            public Guid FormRecordId { get; set; }
            public required string Value { get; set; }
        }
    }
}
