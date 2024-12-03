using MediatR;
using Ykotika.Application.Entities.Form.Commands.Create;

namespace Ykotika.Application.Entities.Form.Record
{
    public class CreateRecordCommand : IRequest<Guid>
    {
        public required Guid FormId { get; set; }
        public required Guid UserId { get; set; }
        public required List<FormInputRecordDto> InputRecords { get; set; }
    }
}
