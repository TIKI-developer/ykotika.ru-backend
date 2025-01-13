using MediatR;
using Ykotika.Application.Models;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetOfferListQuery : IRequest<BaseList<OfferItem>>
    {
        public required SortingDto Sorting { get; set; }
        public required OfferFilterDto Filter { get; set; }
    }
}
