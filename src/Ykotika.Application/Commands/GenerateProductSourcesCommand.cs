using MediatR;

namespace Ykotika.Application.Commands
{
    public class GenerateProductSourcesCommand : IRequest
    {
        public required List<Guid> Products { get; set; }
    }
}
