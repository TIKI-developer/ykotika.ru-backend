using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductListQuery : IRequest<ProductList>
    {
        public bool? IsPublished { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProductTypeId { get; set; }
    }
}
