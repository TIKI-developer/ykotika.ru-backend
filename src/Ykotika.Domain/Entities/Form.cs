using Ykotika.Domain.Interfaces;

namespace Ykotika.Domain.Entities
{
    public class Form : Entity, IHasAuthor, IPublishable
    {
        public required string Name { get; set; }
        public required bool IsPublished { get; set; }
        public required List<Input> Inputs { get; set; }
        public List<FormRecord>? FormRecords { get; set; }
        public required User User { get; set; }

        public class Input
        {
            public required string Id { get; set; }
            public required int OrderIndex { get; set; }
            public required InputType Type { get; set; }
            public required InputExtraAttributes ExtraAttributes { get; set; }
        }
        public class InputExtraAttributes
        {
            public required string Label { get; set; }
            public required string Placeholder { get; set; }
            public required bool IsRequired { get; set; }
            public string[]? Options { get; set; }
            public string? Regex { get; set; }
            public int? MaxSelections { get; set; }
        }
        public enum InputType
        {
            Text,
            Number,
            Textarea,
            Select,
            MultiSelect,
        }
    }
}
