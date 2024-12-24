using MediatR;

namespace Ykotika.Application.Commands.FormRecord
{
    public class DeleteFormRecordCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
