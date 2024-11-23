namespace Ykotika.Application.Common.Exceptions
{
    public class UserAlreadyRegistered(string email)
        : Exception($"User with email: \"{email}\" already registered.")
    { }
}
