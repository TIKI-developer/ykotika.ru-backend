using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetCategoryListQuery : IRequest<CategoryList>
    {
        public bool? IsPublished { get; set; }
    }
}
