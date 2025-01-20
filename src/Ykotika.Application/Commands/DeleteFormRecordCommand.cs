using MediatR;

namespace Ykotika.Application.Commands
{
    public class DeleteFormRecordCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
