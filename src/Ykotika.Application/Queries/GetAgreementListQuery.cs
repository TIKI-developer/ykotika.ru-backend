using MediatR;
using Ykotika.Application.Models;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetAgreementListQuery : IRequest<PagedList<AgreementItem>>
    {
        public required PaginationDto Pagination { get; set; }
        public required SortingDto Sorting { get; set; }
        public required AgreementFilterDto Filter { get; set; }
    }
}
