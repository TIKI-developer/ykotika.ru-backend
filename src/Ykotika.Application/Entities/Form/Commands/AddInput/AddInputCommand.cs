using MediatR;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Commands.AddInput
{
    public class AddInputCommand : IRequest<Guid>
    {
        public Guid FormId { get; set; }
        public required string Label { get; set; }
        public InputType Type { get; set; }
        public bool IsRequired { get; set; }
    }
}
