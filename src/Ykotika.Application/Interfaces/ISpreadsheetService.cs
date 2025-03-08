using Ykotika.Application.Commands;
using Ykotika.Application.Models;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Interfaces
{
    public interface ISpreadsheetService
    {
        FileData GenerateProductsSpreadsheet(List<Product> products, string rootUrl);
        Task<List<ProductFromSpreadsheetDto>> GenerateRequestsFromSpreadsheet(FileData spreadsheet, FileData filesZip);
    }
}
