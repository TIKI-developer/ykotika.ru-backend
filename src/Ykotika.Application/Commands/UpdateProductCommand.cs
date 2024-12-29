using MediatR;
using Ykotika.Application.Models;

namespace Ykotika.Application.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<Guid>? OutsourceShops { get; set; }
        public List<ProductImageDto>? Images { get; set; }
    }
}
