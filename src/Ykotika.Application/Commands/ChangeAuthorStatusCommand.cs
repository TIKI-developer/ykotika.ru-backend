using MediatR;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class ChangeAuthorStatusCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required AuthorStatus NewStatus { get; set; }
    }
}
