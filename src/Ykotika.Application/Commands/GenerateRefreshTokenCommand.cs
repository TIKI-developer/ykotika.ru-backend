using MediatR;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class GenerateRefreshTokenCommand : IRequest<LoginResponse>
    {
        public required Guid UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
