using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetOutsourceShopListQuery : IRequest<BaseList<OutsourceShopItem>>
    {

    }
}
