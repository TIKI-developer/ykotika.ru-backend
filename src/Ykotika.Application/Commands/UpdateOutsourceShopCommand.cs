using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateOutsourceShopCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public string? ImagePath { get; set; }
    }
}
