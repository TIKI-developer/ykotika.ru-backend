using MediatR;

namespace Ykotika.Application.Commands
{
    public class DeleteOutsourceShopCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
