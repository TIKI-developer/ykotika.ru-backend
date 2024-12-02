using MediatR;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Commands.Create
{
    public class CreateFormCommand : IRequest<Guid> { }
}
