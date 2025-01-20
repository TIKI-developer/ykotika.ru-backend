using MediatR;
using Ykotika.Application.Models;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetAuthorListQuery : IRequest<PagedList<AuthorItem>>
    {
        public required PaginationDto Pagination { get; set; }
        public required SortingDto Sorting { get; set; }
        public required AuthorFilterDto Filter { get; set; }
    }
}
