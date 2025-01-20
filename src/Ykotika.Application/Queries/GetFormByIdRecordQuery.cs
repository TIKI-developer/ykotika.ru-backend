using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetFormByIdRecordQuery : IRequest<FormRecordDetails>
    {
        public required Guid Id { get; set; }
    }
}
