using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands
{
    public class GenerateProductSourcesCommandHandler
        (IYkotikaDbContext dbContext,
        IFileService fileService)
        : IRequestHandler<GenerateProductSourcesCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IFileService _fileService = fileService;

        public async Task
            Handle(GenerateProductSourcesCommand request,
                   CancellationToken cancellationToken)
        {
            var products = await
                _dbContext
                .Products
                .Include(e => e.Source)
                .Include(e => e.Images)
                .ThenInclude(e => e.Image)
                .Where(e => request.Products.Contains(e.Id))
                .ToListAsync(cancellationToken)
                ?? throw new Exception("Product list empty!");

            foreach (var product in products)
            {
                var sourceFile = await _fileService.Download(product.Source);

                sourceFile.Path = product.Article + Path.GetExtension(sourceFile.Path);

                await _fileService.Upload(sourceFile, $"Каталог/{product.Article}", false);

                foreach (var image in product.Images)
                {
                    var imageFile = await _fileService.Download(image.Image);

                    imageFile.Path = (image.OrderIndex + 1) + Path.GetExtension(imageFile.Path);

                    await _fileService.Upload(imageFile, $"Каталог/{product.Article}/Фото", false);
                }
            }
        }
    }
}
