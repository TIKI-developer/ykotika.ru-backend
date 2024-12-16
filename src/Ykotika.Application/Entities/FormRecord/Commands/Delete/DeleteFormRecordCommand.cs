using MediatR;

namespace Ykotika.Application.Entities.FormRecord.Commands.Delete
{
    public class DeleteFormRecordCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
