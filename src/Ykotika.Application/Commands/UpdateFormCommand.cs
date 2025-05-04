using MediatR;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateFormCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public bool? IsPublished { get; set; }
        public required List<InputDto> Inputs { get; set; }

        public class InputDto
        {
            public string? Id { get; set; }
            public required Form.InputType Type { get; set; }
            public string? DefaultValue { get; set; }
            public required Form.InputExtraAttributes ExtraAttributes { get; set; }
        }
    }
}
