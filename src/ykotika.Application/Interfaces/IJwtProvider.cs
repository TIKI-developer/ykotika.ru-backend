using Ykotika.Domain;

namespace Ykotika.Application.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(UserModel user);
    }
}
