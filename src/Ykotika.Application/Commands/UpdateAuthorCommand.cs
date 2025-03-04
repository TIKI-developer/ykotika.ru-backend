using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateAuthorCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? About { get; set; }
    }
}
