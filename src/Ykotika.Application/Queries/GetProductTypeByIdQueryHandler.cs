using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    public class GetProductTypeByIdQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetProductTypeByIdQuery, ProductTypeDetails>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductTypeDetails> Handle(GetProductTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var productType = await
                _dbContext
                .ProductTypes
                .Include(e => e.Form)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(ProductType), request.Id);

            return _mapper.Map<ProductTypeDetails>(productType);
        }
    }
}
