namespace Ykotika.Application.Interfaces
{
    public interface INotificationMetadataProvider
    {
        Task<Dictionary<string, string>> EnrichMetadataAsync(string type, Dictionary<string, string> initialMetadata);
    }
}
