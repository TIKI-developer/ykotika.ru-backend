using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class UpdateFormRecordCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required List<UpdateFormInputRecordDto> InputRecords { get; set; }
    }
}
