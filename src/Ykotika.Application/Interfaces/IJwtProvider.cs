using System.Security.Claims;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user, string issuer, string audience);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateEmailVerificationToken(Guid userId, string userEmail);
        bool VerifyEmailToken(string token, Guid userId, string userEmail);
    }
}
