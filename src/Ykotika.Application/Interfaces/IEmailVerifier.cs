namespace Ykotika.Application.Interfaces
{
    public interface IEmailVerifier
    {
        void SendVerificationLink(string userEmail, string link);
    }
}
