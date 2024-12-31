namespace Ykotika.Domain.Entities
{
    public class Form : Entity
    {
        public required string Name { get; set; }
        public required bool IsPublished { get; set; }
        public required List<Input> Inputs { get; set; }
        public List<FormRecord>? FormRecords { get; set; }
        public class Input
        {
            public required string Id { get; set; }
            public required int OrderIndex { get; set; }
            public required string Label { get; set; }
            public required string Placeholder { get; set; }
            public required InputType Type { get; set; }
            public required bool IsRequired { get; set; }
        }
        public enum InputType
        {
            Text,
            File,
        }
    }
}
