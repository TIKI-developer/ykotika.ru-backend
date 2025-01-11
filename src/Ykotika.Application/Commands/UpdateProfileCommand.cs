using MediatR;

namespace Ykotika.Application.Commands
{
    public class UpdateProfileCommand : IRequest
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImagePath { get; set; }
    }
}
