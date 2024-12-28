namespace Ykotika.Domain.Entities
{
    public class Form : Entity
    {
        public required string Name { get; set; }
        public List<Input>? Inputs { get; set; }
        public List<FormRecord>? FormRecords { get; set; }
        public required bool IsPublished { get; set; }
    }
}
