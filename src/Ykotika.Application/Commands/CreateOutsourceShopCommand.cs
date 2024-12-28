using MediatR;

namespace Ykotika.Application.Commands
{
    public class CreateOutsourceShopCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required string Link { get; set; }
        public required Guid LogoFileId { get; set; }
    }
}
