namespace Ykotika.Application.Interfaces
{
    public interface IEmailService
    {
        string GetStringTemplateByName(string name, Dictionary<string, string> placeholders);
        Task Send(string toAddress, string subject, string message);
    }
}
