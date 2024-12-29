using NickBuhro.Translit;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Article
{
    public class ArticleGenerator : IArticleGenerator
    {
        public string Generate(string pattern, FormRecord record)
        {
            string article = "";

            List<string> patternItems = [.. pattern.Split('_')];

            if (record.InputRecords != null)
            {
                foreach (var patternItem in patternItems)
                {
                    string articleItem = record
                        .InputRecords
                        .FirstOrDefault(e => e.FormInput.Label == patternItem)!.Value
                        ?? "";

                    articleItem = Transliteration.CyrillicToLatin(articleItem);

                    article += $"-{articleItem}";
                }
            }

            article = article.TrimStart('-');

            return article;
        }
    }
}
