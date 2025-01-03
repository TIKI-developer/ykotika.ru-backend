namespace Ykotika.Application.Interfaces
{
    public interface IRefreshTokenHasher
    {
        string Encode(string refreshToken);
        bool Verify(string refreshToken, string refreshTokenHash);
    }
}
