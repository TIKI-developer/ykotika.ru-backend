namespace Ykotika.Domain.Entities
{
    public class InputRecord
    {
        public required Guid Id { get; set; }
        public required FormRecord SubmittedFormData { get; set; }
        public required Input FormInput { get; set; }
        public required string Value { get; set; }
    }
}
