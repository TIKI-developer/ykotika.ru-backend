using MediatR;

namespace Ykotika.Application.Commands.Input
{
    public class UpdateInputCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? Label { get; set; }
        public bool? IsRequired { get; set; }
    }
}
