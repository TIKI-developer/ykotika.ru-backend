using MediatR;
using Ykotika.Application.Models;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductListQuery : IRequest<PagedList<ProductItem>>
    {
        public required PaginationDto Pagination { get; set; }
        public required SortingDto Sorting { get; set; }
        public required ProductFilterDto Filter { get; set; }
    }
}
