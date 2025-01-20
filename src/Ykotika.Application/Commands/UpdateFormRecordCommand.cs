using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateFormRecordCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required List<InputRecordDto> InputRecords { get; set; }

        public class InputRecordDto
        {
            public required string Id { get; set; }
            public required string Value { get; set; }
        }
    }
}
