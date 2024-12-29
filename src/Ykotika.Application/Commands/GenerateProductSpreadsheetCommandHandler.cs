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
        : IRequestHandler<GenerateProductSpreadsheetCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly ISpreadsheetService _spreadsheetService = spreadsheetService;
        private readonly IMapper _mapper = mapper;
        private readonly IFileService _fileService = fileService;

        public async Task<Guid> Handle(GenerateProductSpreadsheetCommand request, CancellationToken cancellationToken)
        {
            var products = await
                _dbContext
                .Products
                .Where(e => request.Products.Contains(e.Id))
                .Include(e => e.ProductType)
                .ThenInclude(e => e.Form)
                .ThenInclude(e => e.Inputs)
                .Include(e => e.FormRecord)
                .ThenInclude(e => e.InputRecords)
                .ToListAsync(cancellationToken);

            if (products != null && products.Count > 0)
            {
                var fileData = _spreadsheetService.GenerateProductsTable(products);
                var file = await _fileService.Upload(fileData, "tables", false);
                await _dbContext.Files.AddAsync(file, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return file.Id;
            }
            return Guid.Empty;
        }
    }
}
