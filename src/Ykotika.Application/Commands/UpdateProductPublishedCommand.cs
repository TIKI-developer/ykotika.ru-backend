using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateProductPublishedCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required bool IsPublished { get; set; }
    }
}
