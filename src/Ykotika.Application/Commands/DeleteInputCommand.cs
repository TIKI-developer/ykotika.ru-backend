using MediatR;

namespace Ykotika.Application.Commands
{
    public class DeleteInputCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
