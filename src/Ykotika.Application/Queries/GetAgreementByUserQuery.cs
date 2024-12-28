using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetAgreementByUserQuery : IRequest<AgreementList>
    {
        public required Guid AuthorId { get; set; }
    }
}
