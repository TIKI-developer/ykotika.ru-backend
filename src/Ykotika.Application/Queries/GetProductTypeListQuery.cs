using MediatR;
using Ykotika.Application.Models;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductTypeListQuery : IRequest<PagedList<ProductTypeItem>>
    {
        public required PaginationDto Pagination { get; set; }
        public required SortingDto Sorting { get; set; }
        public required ProductTypeFilterDto Filter { get; set; }
    }
}
