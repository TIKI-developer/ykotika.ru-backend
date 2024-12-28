using MediatR;

namespace Ykotika.Application.Commands
{
    public class DeleteFormCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
