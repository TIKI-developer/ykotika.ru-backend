using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClosedXML.Excel;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.SpreadsheetService
{
    public class SpreadsheetGenerator
        (IMapper mapper)
        : ISpreadsheetService
    {
        private readonly IMapper _mapper = mapper;

        public FileData GenerateProductsTable(List<Product> products, string rootUrl)
        {
            products.ForEach(product => product.Source.Path = Path.Combine(rootUrl, product.Source.Path).Replace("\\", "/"));
            products.ForEach(product => product.Images.ForEach(image => image.Image.Path = Path.Combine(rootUrl, image.Image.Path).Replace("\\", "/")));

            var productsDto = products
                .AsQueryable()
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToList();

            productsDto.ForEach(e => e.AuthorId = Path.Combine(rootUrl, "manage", "admin", "authors", e.AuthorId).Replace("\\", "/"));
            productsDto.ForEach(e => e.Id = Path.Combine(rootUrl, "manage", "moderator", "products", e.Id).Replace("\\", "/"));

            Dictionary<ProductType, List<ProductDto>> productTypeDictionary =
                productsDto
                .GroupBy(product => product.ProductType.Id)
                .ToDictionary(
                    group => productsDto.First(product => product.ProductType.Id == group.Key).ProductType,
                    group => group.ToList()
                );

            foreach (var kvp in productTypeDictionary)
            {
                var productType = kvp.Key;
                var productsOfType = kvp.Value;
            }

            using (var memoryStream = new MemoryStream())
            {
                var workbook = new XLWorkbook();

                foreach (var kvp in productTypeDictionary)
                {
                    var productType = kvp.Key;
                    var productsOfType = kvp.Value;

                    var worksheet = workbook.Worksheets.Add(productType.Name);


                    //Setup headers
                    var headerRow = worksheet.Row(1);
                    Form.Input[] inputs = [.. productType.Form.Inputs.OrderBy(e => e.OrderIndex)];

                    headerRow.Cell(1).Value = "№";
                    headerRow.Cell(2).Value = "Артикул";
                    headerRow.Cell(3).Value = "Название";
                    headerRow.Cell(4).Value = "Описание";
                    headerRow.Cell(5).Value = "18+?";
                    headerRow.Cell(6).Value = "Теги";
                    headerRow.Cell(7).Value = "Исходник";
                    headerRow.Cell(8).Value = "Изображения";
                    headerRow.Cell(9).Value = "Автор";

                    for (int col = 10; col <= inputs.Length + 9; col++)
                    {
                        headerRow.Cell(col).Value = inputs[col - 10].ExtraAttributes.Label;
                    }
                    //
                    var inputOrder = inputs.Select((input, index) => new { input.Id, Index = index })
                       .ToDictionary(x => x.Id, x => x.Index);

                    for (int row = 2; row <= productsOfType.Count + 1; row++)
                    {
                        var product = productsOfType[row - 2];

                        // Get values list
                        var cells = GetCellPropertiesFromDto(product);
                        cells
                        .AddRange(product.FormRecord.InputRecords
                        .OrderBy(e => inputOrder.TryGetValue(e.Id, out var index) ? index : int.MaxValue)
                        .Select(e => new CellDto { Value = e.Value })
                        .ToList());
                        //

                        //Fill values
                        for (int col = 1; col <= cells.Count; col++)
                        {
                            worksheet.Cell(row, col).Value = cells[col - 1].Value;

                            if (col == 1)
                            {
                                worksheet.Cell(row, col).Value = row - 1;
                            }
                            if (col == 9)
                            {
                                worksheet.Cell(row, col).Value = product.User.Email;
                            }
                            if (cells[col - 1].HyperLink != null)
                            {
                                worksheet.Cell(row, col).SetHyperlink(new XLHyperlink(cells[col - 1].HyperLink));
                            }
                        }

                        //Styles
                        worksheet.Rows().AdjustToContents();

                        worksheet.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Row(1).Height = 50;
                        worksheet.Row(1).Style.Font.Bold = true;
                        worksheet.Row(1).Style.Font.FontSize = 15;
                        //

                        worksheet.Columns().AdjustToContents();
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
        private static List<CellDto> GetCellPropertiesFromDto(object dto)
        {
            var cells = new List<CellDto>();

            var properties = dto.GetType()
                .GetProperties()
                .Where(prop => prop.GetCustomAttributes(typeof(CellProperty), false).Any());

            foreach (var property in properties)
            {
                var cellAttribute = (CellProperty)property.GetCustomAttributes(typeof(CellProperty), false).FirstOrDefault()!;

                var value = property.GetValue(dto);
                var cellDto = new CellDto();

                cellDto.Value = value.ToString();

                if (value is ICollection<string> collection)
                {
                    string concatenatedValue = string.Join(";\n", collection);
                    cellDto.Value = concatenatedValue;
                }
                if (cellAttribute.IsHyperLink)
                {
                    cellDto.HyperLink = cellDto.Value;
                }

                cells.Add(cellDto);
            }

            return cells;
        }
    }
}
