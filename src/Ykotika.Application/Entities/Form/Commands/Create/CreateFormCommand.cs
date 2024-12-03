using MediatR;

namespace Ykotika.Application.Entities.Form.Commands.Create
{
    public class CreateFormCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public List<FormInputDto>? Inputs { get; set; }
    }   
}
