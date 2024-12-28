using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetAgreementListQuery : IRequest<AgreementList>
    {
    }
}
