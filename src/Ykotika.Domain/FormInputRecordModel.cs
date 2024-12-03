namespace Ykotika.Domain
{
    public class FormInputRecordModel
    {
        public required Guid Id { get; set; }
        public required FormRecordModel SubmittedFormData { get; set; }
        public required FormInputModel FormInput { get; set; }
        public required string Value { get; set; }
    }
}
