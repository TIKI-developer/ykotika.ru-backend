namespace Ykotika.Email
{
    public class TemplateLoader
    {
        public List<Template> Templates => _templates;
        private readonly List<Template> _templates = [];

        public TemplateLoader(Dictionary<string, string> templatesToInitialize)
        {
            foreach (var templateToInit in templatesToInitialize)
            {
                try
                {
                    var newTemplate = new Template(templateToInit.Key, templateToInit.Value, assembly: typeof(TemplateLoader).Assembly);
                    _templates.Add(newTemplate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки шаблона: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
