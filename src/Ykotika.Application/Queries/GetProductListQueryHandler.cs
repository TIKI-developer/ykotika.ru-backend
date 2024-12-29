using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetProductListQuery, ProductList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductList> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Products
                .Include(e => e.Images)
                .ThenInclude(e => e.File)
                .Include(e => e.FormRecord)
                .ThenInclude(e => e.Author)
                .AsQueryable();

            if (request.IsPublished.HasValue)
            {
                query = query.Where(p => p.IsPublished == request.IsPublished.Value);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(p => p.FormRecord.Author.Id == request.UserId.Value);
            }

            if (request.ProductType.HasValue)
            {
                query = query.Where(p => p.ProductType.Id == request.ProductType.Value);
            }

            var products = await query
                .ProjectTo<ProductItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductList { Products = products };
        }

    }
}
