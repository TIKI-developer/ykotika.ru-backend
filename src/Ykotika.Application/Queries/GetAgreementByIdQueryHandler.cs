using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    public class GetAgreementByIdQueryHandler 
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetAgreementByIdQuery, AgreementDetails>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<AgreementDetails> Handle(GetAgreementByIdQuery request, CancellationToken cancellationToken)
        {
            var agreement = await
                _dbContext
                .Agreements
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Agreement), request.Id);

            return _mapper.Map<AgreementDetails>(agreement);
        }
    }
}
