namespace Ykotika.Application.Entities.FormRecord.Commands.Update
{
    public class UpdateFormInputRecordDto
    {
        public required Guid Id { get; set; }
        public required string Value { get; set; }
    }
}
