using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateChatCommand : IRequest<Guid>
    {
        public string? Name { get; set; }
        public required List<Guid> Members { get; set; }
    }
}
