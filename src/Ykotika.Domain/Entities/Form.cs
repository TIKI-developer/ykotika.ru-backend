using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class Form : Entity
    {
        public required string Name { get; set; }
        public List<Input>? Inputs { get; set; }
        public List<FormRecord>? SubmittedForms { get; set; }
        public required Timestamps Timestamps { get; set; }
    }
}
