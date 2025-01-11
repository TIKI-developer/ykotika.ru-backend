using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetOfferListQuery : IRequest<OfferList>
    {
        public bool? IsPublished { get; set; }
    }
}
