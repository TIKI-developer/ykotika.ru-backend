using NickBuhro.Translit;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Article
{
    public class ArticleGenerator : IArticleGenerator
    {
        public string Generate(List<string> pattern, Product product)
        {
            string article = "";
            FormRecord record = product.FormRecord;
            if (record.InputRecords != null)
            {
                foreach (var patternItem in pattern)
                {
                    string articleItem = patternItem switch
                    {
                        "name" => product.Name,
                        _ => record
                            .InputRecords
                            .FirstOrDefault(e =>
                            e.Id == patternItem)!.Value
                            ?? "",
                    };
                    articleItem = Transliteration.CyrillicToLatin(articleItem);

                    article += $"-{articleItem}";
                }
            }

            article = article.TrimStart('-');

            return article;
        }
    }
}
