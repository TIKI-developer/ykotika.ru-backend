using System.Reflection;
using System.Text;

namespace Ykotika.Email
{
    public class Template
    {
        public string ResourceName => _resourceName;
        public string Name => _name;

        private readonly string _resourceName;
        private readonly string _string;
        private readonly string _name;

        public Template(string name, string resourceName, Assembly? assembly = null)
        {
            _name = name;
            _resourceName = resourceName;
            assembly ??= typeof(Template).Assembly;

            var fullResourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(name => name.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase))
                ?? throw new FileNotFoundException($"Ресурс с именем {resourceName} не найден в сборке.");

            using var stream = assembly.GetManifestResourceStream(fullResourceName)
            ?? throw new InvalidOperationException($"Не удалось получить поток для ресурса {fullResourceName}.");

            using var reader = new StreamReader(stream, Encoding.UTF8);

            _string = reader.ReadToEnd();
        }
        public string Get(Dictionary<string, string> placeholders)
        {
            string value = "";

            foreach (var placeholder in placeholders)
            {
                value = _string.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }

            return value;
        }
    }
}
