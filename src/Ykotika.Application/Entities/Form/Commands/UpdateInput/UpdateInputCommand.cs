using MediatR;

namespace Ykotika.Application.Entities.Form.Commands.UpdateInput
{
    public class UpdateInputCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? Label { get; set; }
        public bool? IsRequired { get; set; }
    }
}
