using ClosedXML.Excel;
using System.IO;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.SpreadsheetService
{
    public class SpreadsheetGenerator : ISpreadsheetService
    {
        public FileData GenerateProductsTable(List<Product> products)
        {
            List<ProductType> productTypes = 
                products
                .Select(x => x.ProductType)
                .Distinct()
                .ToList();
            var workbook = new XLWorkbook();

            foreach (var productType in productTypes)
            {
                var worksheet = workbook.Worksheets.Add(productType.Name);

                var headerRow = worksheet.Row(1);
                Input[] inputs = [.. productType.Form.Inputs];

                for (int col = 1; col <= inputs.Length; col++)
                {
                    headerRow.Cell(col).Value = inputs[col - 1].Label;
                }
                Console.WriteLine("hello");
            }
                Console.WriteLine("hi");
            workbook.SaveAs("output.xlsx");
            return null;
        }
        public FileData Generate<T>(List<T> dto)
        {
            if (dto == null || dto.Count == 0)
                throw new ArgumentException("DTO list cannot be null or empty.");
            using (var memoryStream = new MemoryStream())
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Data");

                var properties = dto.First().GetType().GetProperties();
                var headerRow = worksheet.Row(1);

                for (int col = 1; col <= properties.Length; col++)
                {
                    headerRow.Cell(col).Value = properties[col - 1].Name;
                }

                for (int row = 2; row <= dto.Count + 1; row++)
                {
                    var currentItem = dto[row - 2];
                    var currentProperties = currentItem.GetType().GetProperties();

                    for (int col = 1; col <= currentProperties.Length; col++)
                    {
                        var value = currentProperties[col - 1].GetValue(currentItem);
                        worksheet.Cell(row, col).Value = value.ToString();
                    }
                }

                worksheet.Rows().AdjustToContents();
                worksheet.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                workbook.SaveAs(memoryStream);

                var fileContent = memoryStream.ToArray();

                return new FileData
                {
                    Name = $"Table {DateTime.UtcNow.Ticks}.xlsx",
                    Content = fileContent,
                };
            }

        }
    }
}
