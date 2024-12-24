using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.FormRecord
{
    public class GetFormRecordListQuery : IRequest<FormRecordListViewModel> { }
}
