using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateProductTypeCommand : IRequest<Guid>
    {
        public required Guid FormId { get; set; }
        public required string Name { get; set; }
        public required string ArticlePattern { get; set; }
    }
}
