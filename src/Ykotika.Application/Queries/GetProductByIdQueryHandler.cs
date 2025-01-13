using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    public class GetProductByIdQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetProductByIdQuery, ProductDetails>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductDetails> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await
                _dbContext
                .Products
                .Include(e => e.Images)
                .ThenInclude(e => e.Image)
                .Include(e => e.OutsourceShops)
                .Include(e => e.Tags)
                .Include(e => e.Categories)
                .Include(e => e.ProductType)
                .Include(e => e.FormRecord)
                .ThenInclude(e => e.Form)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), request.Id);

            return _mapper.Map<ProductDetails>(product);
        }
    }
}
