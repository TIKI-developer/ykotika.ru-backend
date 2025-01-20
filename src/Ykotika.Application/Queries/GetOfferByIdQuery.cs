using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetOfferByIdQuery : IRequest<OfferDetails>
    {
        public required Guid Id { get; set; }
    }
}
