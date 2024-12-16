namespace Ykotika.Domain
{
    public class FormModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public List<FormInputModel>? Inputs { get; set; }
        public List<FormRecordModel>? SubmittedForms { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}
