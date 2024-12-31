using MediatR;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class CreateFormCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required List<InputDto> Inputs { get; set; }

        public class InputDto
        {
            public required string Label { get; set; }
            public required string Placeholder { get; set; }
            public required Form.InputType Type { get; set; }
            public required bool IsRequired { get; set; }
        }
    }
}
