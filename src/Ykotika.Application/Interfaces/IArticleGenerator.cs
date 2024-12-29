using Ykotika.Domain.Entities;

namespace Ykotika.Application.Interfaces
{
    public interface IArticleGenerator
    {
        string Generate(string pattern, FormRecord record);
    }
}
