using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class VerifyEmailCommand : IRequest<LoginResponse>
    {
        public required Guid UserId { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}
