using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateProductTypeCommand : IRequest<Guid>
    {
        public required Guid FormId { get; set; }
        public required string Name { get; set; }
        public required List<string> ArticlePattern { get; set; }
    }
}
