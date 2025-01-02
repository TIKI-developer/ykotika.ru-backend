using Ykotika.Application.Interfaces;

namespace Ykotika.Security
{
    public class RefreshTokenHasher : IRefreshTokenHasher
    {
        public string Encode(string refreshToken)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(refreshToken);
        }
        public bool Verify(string refreshToken, string refreshTokenHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(refreshToken, refreshTokenHash);
        }
    }
}
