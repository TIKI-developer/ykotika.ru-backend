namespace Ykotika.NotificationSystem
{
    public class RouteTemplate
    {
        public string Type { get; set; } = default!;
        public Dictionary<string, string> Conditions { get; set; } = new();
        public string Template { get; set; } = default!;
    }
}
