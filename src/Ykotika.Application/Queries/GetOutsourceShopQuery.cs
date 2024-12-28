using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetOutsourceShopQuery : IRequest<OutsourceShopDetails>
    {
        public required Guid Id { get; set; }
    }
}
