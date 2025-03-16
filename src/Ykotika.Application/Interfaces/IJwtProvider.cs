using System.Security.Claims;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user, string? issuer = null, string? audience = null);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateEmailVerificationToken(Guid userId, string userEmail);
        bool VerifyEmailToken(string token, Guid userId, string userEmail);
        string GeneratePasswordRecoverToken(string userEmail);
        string VerifyPasswordRecoverToken(string token);
    }
}
