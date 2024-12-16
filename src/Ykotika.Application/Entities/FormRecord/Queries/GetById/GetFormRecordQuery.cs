using MediatR;

namespace Ykotika.Application.Entities.FormRecord.Queries.GetById
{
    public class GetFormRecordQuery : IRequest<FormRecordViewModel>
    {
        public required Guid Id { get; set; }
    }
}
