using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Interfaces
{
    public interface ISpreadsheetService
    {
        FileData GenerateProductsTable(List<Product> products, string rootUrl);
    }
}
