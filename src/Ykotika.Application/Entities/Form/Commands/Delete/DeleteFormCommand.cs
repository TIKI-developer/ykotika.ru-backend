using MediatR;

namespace Ykotika.Application.Entities.Form.Commands.Delete
{
    public class DeleteFormCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
