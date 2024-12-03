using MediatR;

namespace Ykotika.Application.Entities.FormRecord.Commands.Update
{
    public class UpdateFormRecordCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required List<UpdateFormInputRecordDto> InputRecords { get; set; }
    }
}
