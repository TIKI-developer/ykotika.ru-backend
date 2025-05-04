using MediatR;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class CreateFormCommand : IRequest<Guid>
    {
        public required Guid AuthorId { get; set; }
        public required string Name { get; set; }
        public required bool IsPublished { get; set; }
        public required List<InputDto> Inputs { get; set; }

        public class InputDto
        {
            public required Form.InputType Type { get; set; }
            public string? DefaultValue { get; set; }
            public required Form.InputExtraAttributes ExtraAttributes { get; set; }
        }
    }
}
