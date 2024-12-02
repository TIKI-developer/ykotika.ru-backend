namespace Ykotika.Domain
{
    public class FormInputModel
    {
        public required Guid Id { get; set; }
        public required FormModel Form { get; set; }
        public required string Label { get; set; }
        public required InputType Type { get; set; }
        public bool IsRequired { get; set; }
        public List<FormInputRecordModel>? SubmittedFormFieldsData { get; set; }
    }

    public enum InputType
    {
        Text
    }
}
