using MediatR;

namespace Ykotika.Application.Commands
{
    public class ChangeProductOutsourceShopCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required List<Guid> OutsourceShops { get; set; }
    }
}
