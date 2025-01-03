using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateFormCommandHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        :
        IRequestHandler<UpdateFormCommand>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateFormCommand request, CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .Include(f => f.Inputs)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Form), request.Id);

            form.Name = request.Name ?? form.Name;

            if (request.Inputs != null)
            {
                foreach (var (input, index) in request.Inputs.Select((input, index) => (input, index)))
                {
                    var formInput = form
                    .Inputs
                    .FirstOrDefault(e => e.Id == input.Id);
                    if (formInput == null)
                    {
                        var newInput = new Form.Input
                        {
                            Id = Nanoid.Generate(size: 6),
                            OrderIndex = index,
                            Label = input.Label,
                            Placeholder = input.Placeholder,
                            Type = input.Type,
                            IsRequired = input.IsRequired
                        };
                        form.Inputs.Add(newInput);
                    }
                    else
                    {
                        formInput.Label = input.Label ?? formInput.Label;
                        formInput.OrderIndex = index;
                        formInput.Placeholder = input.Placeholder ?? formInput.Placeholder;
                        formInput.IsRequired = input.IsRequired;
                        formInput.Type = input.Type;
                    }
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
