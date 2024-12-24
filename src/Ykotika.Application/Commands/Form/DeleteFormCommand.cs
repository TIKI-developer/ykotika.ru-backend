using MediatR;

namespace Ykotika.Application.Commands.Form
{
    public class DeleteFormCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
