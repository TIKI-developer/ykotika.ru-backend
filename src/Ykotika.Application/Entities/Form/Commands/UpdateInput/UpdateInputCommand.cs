using MediatR;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Commands.UpdateInput
{
    public class UpdateInputCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required string Label { get; set; }
        public InputType Type { get; set; }
        public bool IsRequired { get; set; }
    }
}
