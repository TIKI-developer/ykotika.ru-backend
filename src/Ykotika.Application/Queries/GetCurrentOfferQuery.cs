using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetCurrentOfferQuery : IRequest<CurrentOfferDetails>
    {
        public Guid? UserId { get; set; }
    }
}
