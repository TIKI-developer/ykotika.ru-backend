namespace Ykotika.Domain.Entities
{
    public class FormRecord : Entity
    {
        public required Form Form { get; set; }
        public required User Author { get; set; }
        public required List<InputRecord> InputRecords { get; set; } = [];

        public class InputRecord
        {
            public required string Id { get; set; }
            public required string Value { get; set; }
        }
    }
}
