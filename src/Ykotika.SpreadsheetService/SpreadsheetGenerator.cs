using ClosedXML.Excel;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.SpreadsheetService
{
    public class SpreadsheetGenerator : ISpreadsheetService
    {
        public FileData GenerateProductsTable(List<Product> products)
        {
            Dictionary<ProductType, List<Product>> productTypeDictionary =
                products
                .GroupBy(product => product.ProductType)
                .ToDictionary(group => group.Key, group => group.ToList());

            using (var memoryStream = new MemoryStream())
            {
                var workbook = new XLWorkbook();

                foreach (var kvp in productTypeDictionary)
                {
                    var productType = kvp.Key;
                    var productsOfType = kvp.Value;

                    var worksheet = workbook.Worksheets.Add(productType.Name);

                    var headerRow = worksheet.Row(1);
                    Form.Input[] inputs = [.. productType.Form.Inputs];

                    headerRow.Cell(1).Value = "Артикул";
                    headerRow.Cell(2).Value = "Название";
                    headerRow.Cell(3).Value = "Описание";
                    headerRow.Cell(4).Value = "Теги";
                    headerRow.Cell(5).Value = "Исходник";
                    headerRow.Cell(6).Value = "Изображения";

                    for (int col = 7; col <= inputs.Length + 6; col++)
                    {
                        headerRow.Cell(col).Value = inputs[col - 7].ExtraAttributes.Label;
                    }

                    for (int row = 2; row <= productsOfType.Count + 1; row++)
                    {
                        var product = productsOfType[row - 2];

                        worksheet.Cell(row, 1).Value = product.Article;
                        worksheet.Cell(row, 2).Value = product.Name;
                        worksheet.Cell(row, 3).Value = product.Description;
                        worksheet.Cell(row, 4).Value = string.Join(", ", product.Tags.Select(e => e.Value));
                        worksheet.Cell(row, 5).Value = product.Source.Path;
                        worksheet.Cell(row, 6).Value = string.Join(", ", product.Images.Select(e => e.Image.Path));


                        var productInputRecords = product.FormRecord.InputRecords.ToArray();
                        for (int col = 7; col <= inputs.Length + 6; col++)
                        {
                            var propertyValue = productInputRecords[col - 7].Value;
                            worksheet.Cell(row, col).Value = propertyValue?.ToString();
                        }
                    }
                }

                workbook.SaveAs(memoryStream);

                var fileContent = memoryStream.ToArray();

                return new FileData
                {
                    Path = $"Table {DateTime.UtcNow.Ticks}.xlsx",
                    Content = fileContent,
                };
            }
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
                    Path = $"Table {DateTime.UtcNow.Ticks}.xlsx",
                    Content = fileContent,
                };
            }

        }
    }
}
