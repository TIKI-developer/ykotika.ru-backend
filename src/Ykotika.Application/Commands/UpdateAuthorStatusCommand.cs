using MediatR;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateAuthorStatusCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required AuthorStatus NewStatus { get; set; }
    }
}
