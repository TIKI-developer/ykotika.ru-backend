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

            List<string> patternItems = [.. pattern.Split('-')];

            if (record.InputRecords != null)
            {
                foreach (var patternItem in patternItems)
                {
                    string articleItem =
                        record
                        .InputRecords
                        .FirstOrDefault(e =>
                        e.Id == record
                                .Form
                                .Inputs
                                .FirstOrDefault(e => e.Label == patternItem).Id)!.Value
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
