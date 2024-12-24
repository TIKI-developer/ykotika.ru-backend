using MediatR;

namespace Ykotika.Application.Commands.Input
{
    public class DeleteInputCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
