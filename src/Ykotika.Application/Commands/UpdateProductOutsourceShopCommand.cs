using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateProductOutsourceShopCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required List<Guid> OutsourceShops { get; set; }
    }
}
