using Ykotika.Domain;

namespace Ykotika.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(UserModel user);
        string GenerateEmailVerificationToken(Guid userId, string userEmail);
        bool VerifyEmailToken(string token, Guid userId, string userEmail);
    }
}
