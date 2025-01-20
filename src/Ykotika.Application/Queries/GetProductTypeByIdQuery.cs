using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductTypeByIdQuery : IRequest<ProductTypeDetails>
    {
        public required Guid Id { get; set; }
    }
}
