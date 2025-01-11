using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    public class GetCurrentOfferQueryHandler 
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetCurrentOfferQuery, CurrentOfferDetails>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<CurrentOfferDetails> 
            Handle(GetCurrentOfferQuery request, CancellationToken cancellationToken)
        {
            var offer = await
                _dbContext
                .Offers
                .OrderByDescending(e => e.Timestamps.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(nameof(Offer), "current");

            var vm = _mapper.Map<CurrentOfferDetails>(offer);

            if (request.UserId != null)
            {
                var agreement = await
                    _dbContext
                    .Agreements
                    .FirstOrDefaultAsync(e => e.Offer.Id == offer.Id &&
                                         e.Author.Id == request.UserId);
                vm.IsAccepted = agreement != null;
            }

            return vm;
        }
    }
}
