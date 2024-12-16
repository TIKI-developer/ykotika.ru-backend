using MediatR;

namespace Ykotika.Application.Entities.FormRecord.Commands.Create
{
    public class CreateFormRecordCommand : IRequest<Guid>
    {
        public required Guid FormId { get; set; }
        public required Guid UserId { get; set; }
        public required List<CreateFormInputRecordDto> InputRecords { get; set; }
    }
}
