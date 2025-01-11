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
                .Include(e => e.Author)
                .Include(e => e.Images)
                .ThenInclude(e => e.Image)
                .Include(e => e.FormRecord)
                .Include(e => e.OutsourceShops)
                .AsQueryable();

            if (request.IsPublished.HasValue)
            {
                query = query.Where(p => p.IsPublished == request.IsPublished.Value);
            }

            if (request.AuthorId.HasValue)
            {
                query = query.Where(p => p.Author.Id == request.AuthorId.Value);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(p => p.Author.Id == request.UserId.Value);
            }

            if (request.ProductTypeId.HasValue)
            {
                query = query.Where(p => p.ProductType.Id == request.ProductTypeId.Value);
            }

            var products = await query
                .ProjectTo<ProductItem>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new ProductList { Products = products };
        }

    }
}
