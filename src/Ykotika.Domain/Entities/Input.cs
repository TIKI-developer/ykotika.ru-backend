namespace Ykotika.Domain.Entities
{
    public class Input
    {
        public required Guid Id { get; set; }
        public required Form Form { get; set; }
        public required int OrderIndex { get; set; }
        public required string Label { get; set; }
        public required InputType Type { get; set; }
        public bool IsRequired { get; set; }
        public List<InputRecord>? SubmittedFormFieldsData { get; set; }
    }

    public enum InputType
    {
        Text,
        File,
    }
}
