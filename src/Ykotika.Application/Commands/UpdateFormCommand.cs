using MediatR;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateFormCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public List<InputDto>? Inputs { get; set; }

        public class InputDto
        {
            public required string Id { get; set; }
            public required string Label { get; set; }
            public required string Placeholder { get; set; }
            public required Form.InputType Type { get; set; }
            public required bool IsRequired { get; set; }
        }
    }
}
