using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateFormCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
