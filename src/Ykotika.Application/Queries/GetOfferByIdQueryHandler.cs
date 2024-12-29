using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    public class GetOfferByIdQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetOfferByIdQuery, OfferDetails>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<OfferDetails> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
        {
            var offer = await
                _dbContext
                .Offers
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Offer), request.Id);

            return _mapper.Map<OfferDetails>(offer);
        }
    }
}
