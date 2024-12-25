using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class FormRecord : Entity
    {
        public required Form Form { get; set; }
        public required User Author { get; set; }
        public List<InputRecord> InputRecords { get; set; } = [];
    }
}
