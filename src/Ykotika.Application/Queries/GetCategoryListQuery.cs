using MediatR;
using Ykotika.Application.Models;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetCategoryListQuery : IRequest<PagedList<CategoryItem>>
    {
        public required PaginationDto Pagination { get; set; }
        public required SortingDto Sorting { get; set; }
        public required PublishableFilterDto Filter { get; set; }
    }
}
