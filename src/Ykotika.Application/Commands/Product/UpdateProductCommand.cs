using MediatR;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands.Product
{
    public class UpdateProductCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<OutsourceShop>? OutsourceShops { get; set; }
        public List<Domain.Entities.File>? Images { get; set; }
    }
}
