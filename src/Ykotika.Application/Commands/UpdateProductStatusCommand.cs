using MediatR;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateProductStatusCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required ProductStatus NewStatus { get; set; }
    }
}
