using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateFormRecordCommand : IRequest<Guid>
    {
        public required Guid FormId { get; set; }
        public required Guid UserId { get; set; }
        public required List<InputRecordDto> InputRecords { get; set; }

        public class InputRecordDto
        {
            public required string Id { get; set; }
            public required string Value { get; set; }
        }
    }
}
