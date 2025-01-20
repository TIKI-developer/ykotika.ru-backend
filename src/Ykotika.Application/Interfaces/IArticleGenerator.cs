using Ykotika.Domain.Entities;

namespace Ykotika.Application.Interfaces
{
    public interface IArticleGenerator
    {
        string Generate(List<string> pattern, Product product);
    }
}
