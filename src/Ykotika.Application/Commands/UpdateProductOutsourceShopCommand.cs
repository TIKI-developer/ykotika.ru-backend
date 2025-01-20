using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateProductOutsourceShopCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required List<OutsourceShopLinkDto> OutsourceShopInfo { get; set; }
        public class OutsourceShopLinkDto
        {
            public required Guid OutsourceShopId { get; set; }
            public required string Link { get; set; }
        }
    }
}
