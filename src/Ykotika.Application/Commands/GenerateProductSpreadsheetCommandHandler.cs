using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands
{
    public class GenerateProductSpreadsheetCommandHandler
        (IYkotikaDbContext dbContext,
        ISpreadsheetService spreadsheetService,
        IMapper mapper,
        IFileService fileService)
        : IRequestHandler<GenerateProductSpreadsheetCommand, string>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly ISpreadsheetService _spreadsheetService = spreadsheetService;
        private readonly IMapper _mapper = mapper;
        private readonly IFileService _fileService = fileService;

        public async Task<string>
            Handle(GenerateProductSpreadsheetCommand request,
                   CancellationToken cancellationToken)
        {
            var products = await
                _dbContext
                .Products
                .Where(e => request.Products.Contains(e.Id))
                .Include(e => e.ProductType)
                .ThenInclude(e => e.Form)
                .ThenInclude(e => e.Inputs)
                .ThenInclude(e => e.ExtraAttributes)
                .Include(e => e.FormRecord)
                .ThenInclude(e => e.InputRecords)
                .Include(e => e.User)
                .Include(e => e.Source)
                .Include(e => e.Images)
                .ThenInclude(e => e.Image)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (products != null && products.Count > 0)
            {
                var fileData = _spreadsheetService.GenerateProductsSpreadsheet(products, request.RootUrl);
                var file = await _fileService.Upload(fileData, "tables", false);
                await _dbContext.Files.AddAsync(file, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return file.Path;
            }
            return string.Empty;
        }
    }
}
