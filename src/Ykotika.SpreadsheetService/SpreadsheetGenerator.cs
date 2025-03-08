using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClosedXML.Excel;
using SharpCompress.Archives;
using System.Text.Json;
using Ykotika.Application.Commands;
using Ykotika.Application.Interfaces;
using Ykotika.Application.Models;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;
using static Ykotika.Application.Models.ProductFromSpreadsheetDto;

namespace Ykotika.SpreadsheetService
{
    public class SpreadsheetGenerator
        (IMapper mapper,
        IFileService fileService)
        : ISpreadsheetService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IFileService _fileService = fileService;

        public FileData GenerateProductsSpreadsheet(List<Product> products, string rootUrl)
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
                    string concatenatedValue = string.Join(";", collection);
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

        public async Task<List<ProductFromSpreadsheetDto>> GenerateRequestsFromSpreadsheet(FileData spreadsheet, FileData filesZip)
        {
            var filesFromZips = new List<FilesFromZipForSpreadsheet>();

            using (var stream = new MemoryStream(filesZip.Content))
            {
                using var archive = ArchiveFactory.Open(stream);

                foreach (var entry in archive.Entries.Where(e => e.IsDirectory))
                {
                    string articleFolder = entry.Key.Trim('/');
                    string article = articleFolder.Split('/')[0];
                    var files = archive.Entries
                        .Where(e => e.Key.StartsWith(articleFolder) && !e.IsDirectory)
                        .ToList();

                    if (files.Count == 0) continue;

                    var sourceFile = files.FirstOrDefault();
                    if (sourceFile == null) continue;

                    var sourceFileEntity = await _fileService.Upload(new FileData
                    { 
                        Content = ReadEntryBytes(sourceFile),
                        Path = "static"
                    });



                    var images = new List<ImageListItem>();

                    string photoFolder = $"{articleFolder}/Фото/";
                    var photoFiles = files
                        .Where(e => e.Key.StartsWith(photoFolder) && (e.Key.EndsWith(".jpg") || e.Key.EndsWith(".png") || e.Key.EndsWith(".jpeg")))
                        .OrderBy(e => e.Key)
                        .ToList();

                    int orderIndex = 0;

                    foreach (var photoFile in photoFiles)
                    {
                        var imageFile = await _fileService.Upload(new FileData
                        {
                            Content = ReadEntryBytes(photoFile),
                            Path = "static"
                        });

                        images.Add(new ImageListItem
                        {
                            Image = imageFile,
                            OrderIndex = orderIndex 
                        });
                        orderIndex++;
                    }

                    filesFromZips.Add(new FilesFromZipForSpreadsheet
                    {
                        Article = article,
                        Source = sourceFileEntity,
                        Images = images,
                    });
                }
            }

            var productCommands = new List<ProductFromSpreadsheetDto>();

            using (var stream = new MemoryStream(spreadsheet.Content)) 
            using (var workbook = new XLWorkbook(stream))
            {
                foreach (var worksheet in workbook.Worksheets)
                {
                    if (worksheet.Position.Equals(1))
                    {
                        continue;
                    }

                    var lastRow = worksheet.LastRowUsed().RowNumber();
                    var lastCol = worksheet.LastColumnUsed().ColumnNumber();

                    for (int row = 2; row <= lastRow; row++)
                    {
                        var inputRecords = new List<ProductFromSpreadsheetDto.FormRecordFromSpreadsheet.InputRecordFromSpreadsheet>();

                        for (int col = 13;  col <= lastCol; col++)
                        {
                            inputRecords.Add(new ProductFromSpreadsheetDto.FormRecordFromSpreadsheet.InputRecordFromSpreadsheet
                            { 
                                Name = worksheet.Cell(1, col).GetValue<string>(),
                                Value = worksheet.Cell(row, col).GetValue<string>()
                            });
                        }
                        ProductStatus status = ProductStatus.Edit;

                        switch (worksheet.Cell(row, 10).GetValue<string>().Replace(" ", "").Replace("\r", "").Replace("\n", ""))
                        {
                            case "Редактирование":
                                status = ProductStatus.Edit;
                                break;
                            case "Ожидание модерации":
                                status = ProductStatus.PendingModeration;
                                break;
                            case "Модерируется":
                                status = ProductStatus.Moderating;
                                break;
                            case "Завершен":
                                status = ProductStatus.Done;
                                break;
                            case "Продается":
                                status = ProductStatus.Selling;
                                break;
                            case "Не продается":
                                status = ProductStatus.NotSelling;
                                break;
                            case "Некорректный":
                                status = ProductStatus.Incorrect;
                                break;
                            case "Исправлен":
                                status = ProductStatus.Fixed;
                                break;
                        }
                        var filesFromZip = filesFromZips.FirstOrDefault(e => e.Article == worksheet.Cell(row, 1).GetValue<string>().Replace(" ", "").Replace("\r", "").Replace("\n", ""));

                        if (filesFromZip == null) 
                        {
                            Console.WriteLine($"Отсутствуют файлы товара: {worksheet.Cell(row, 1).GetValue<string>()}");
                            continue; 
                        }

                        var product = new ProductFromSpreadsheetDto
                        {
                            ProductTypeName = worksheet.Name,
                            Article = worksheet.Cell(row, 1).GetValue<string>(),
                            Name = worksheet.Cell(row, 2).GetValue<string>(),
                            Description = worksheet.Cell(row, 3).GetValue<string>(),
                            UserEmail = worksheet.Cell(row, 4).GetValue<string>(),
                            IsAdult = worksheet.Cell(row, 5).GetValue<string>() == "Да",
                            Tags = [.. worksheet.Cell(row, 6).GetValue<string>().Split(";")],
                            CategoryNames = [.. worksheet.Cell(row, 7).GetValue<string>().Split(";")],
                            Files = filesFromZip,
                            Status = status,
                            IsPublished = worksheet.Cell(row, 11).GetValue<string>() == "Да",
                            OutsourceShops = JsonSerializer.Deserialize<List<OutsourceShopFromSpreadsheet>>(worksheet.Cell(row, 12).GetValue<string>()),
                            FormRecord = new FormRecordFromSpreadsheet 
                            { 
                                InputRecords = inputRecords,
                                UserEmail = worksheet.Cell(row, 4).GetValue<string>(),
                                IsPublished = worksheet.Cell(row, 11).GetValue<string>() == "Да"
                            }
                        };

                        productCommands.Add(product);
                    }
                }
            }

            return productCommands;
        }
        static byte[] ReadEntryBytes(IArchiveEntry entry)
        {
            using var stream = entry.OpenEntryStream();
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
