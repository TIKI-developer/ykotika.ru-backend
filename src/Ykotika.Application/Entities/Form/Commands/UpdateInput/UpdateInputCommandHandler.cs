using MediatR;

namespace Ykotika.Application.Entities.Form.Commands.UpdateInput
{
    public class UpdateInputCommandHandler : IRequestHandler<UpdateInputCommand>
    {
        public Task Handle(UpdateInputCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
