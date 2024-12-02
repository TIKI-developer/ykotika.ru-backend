using MediatR;
using Ykotika.Application.Entities.Form.Commands.Create;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Commands.Update
{
    public class UpdateFormCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public List<FormInputDto>? Fields { get; set; }
    }
}
