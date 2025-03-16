using MediatR;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class UpdateAuthorCommand : IRequest
    {
        public required Guid Id { get; set; }
        public List<Social>? Socials { get; set; }
        public string? About { get; set; }
    }
}
