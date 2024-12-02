using MediatR;

namespace Ykotika.Application.Entities.Form.Commands.DeleteInput
{
    public class DeleteInputCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
