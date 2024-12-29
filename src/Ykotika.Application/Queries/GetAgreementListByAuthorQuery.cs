using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetAgreementListByAuthorQuery : IRequest<AgreementList>
    {
        public required Guid Id { get; set; }
    }
}
