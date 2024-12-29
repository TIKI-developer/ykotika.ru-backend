using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetAgreementByIdQuery : IRequest<AgreementDetails>
    {
        public required Guid Id { get; set; }
    }
}
