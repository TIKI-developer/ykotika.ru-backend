using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDetails>
    {
        public required Guid Id { get; set; }
    }
}
