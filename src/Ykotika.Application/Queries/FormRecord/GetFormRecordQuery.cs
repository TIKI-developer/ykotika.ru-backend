using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.FormRecord
{
    public class GetFormRecordQuery : IRequest<FormRecordViewModel>
    {
        public required Guid Id { get; set; }
    }
}
