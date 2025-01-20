namespace Ykotika.Application.Interfaces
{
    public interface IEmailVerifier
    {
        Task SendVerificationLinkAsync(string userEmail, string link);
    }
}
