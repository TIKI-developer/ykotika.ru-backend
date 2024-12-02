using MediatR;

namespace Ykotika.Application.Entities.Form.Commands.DeleteInput
{
    public class DeleteInputCommandHandler : IRequestHandler<DeleteInputCommand>
    {
        public Task Handle(DeleteInputCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
