using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Interfaces
{
    public interface ISpreadsheetService
    {
        FileData Generate<T>(List<T> dto);
        FileData GenerateProductsTable(List<Product> products);
    }
}
