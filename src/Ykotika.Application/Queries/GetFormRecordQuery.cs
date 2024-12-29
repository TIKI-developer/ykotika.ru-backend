using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetFormRecordQuery : IRequest<FormRecordDetails>
    {
        public required Guid Id { get; set; }
    }
}
