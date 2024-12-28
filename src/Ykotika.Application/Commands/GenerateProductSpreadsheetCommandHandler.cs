using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.Models;
using Ykotika.Domain.Entities;

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
                //.ProjectTo<ProductSpreadsheetDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            Console.WriteLine(products.Count);
            if (products != null && products.Count > 0)
            {
                //var fileData = _spreadsheetService.Generate(products);
                //var file = await _fileService.Upload(fileData, "tables", false);

                //await _dbContext.Files.AddAsync(file, cancellationToken);
                //await _dbContext.SaveChangesAsync(cancellationToken);

                //return file.Id;
                _spreadsheetService.GenerateProductsTable(products);

                return Guid.Empty;
            }
            return Guid.Empty;
        }
    }
}
