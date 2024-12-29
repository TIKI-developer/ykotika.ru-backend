using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductTypeListQuery : IRequest<ProductTypeList>
    {
        public bool? IsPublished { get; set; }
    }
}
