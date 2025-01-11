using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetAgreementListQuery : IRequest<AgreementList>
    {
        public Guid? AuthorId { get; set; }
        public Guid? OfferId { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
    }
}
