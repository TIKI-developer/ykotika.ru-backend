namespace Ykotika.Domain
{
    public class FormRecordModel
    {
        public required Guid Id { get; set; }
        public required FormModel Form { get; set; }
        public required UserModel User { get; set; }
        public required List<FormInputRecordModel> InputRecords { get; set;}
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}
