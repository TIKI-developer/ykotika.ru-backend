using AutoMapper;
using MediatR;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands.Form
{
    public class CreateFormCommandHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        :
        IRequestHandler<CreateFormCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            var inputs = _mapper.Map<List<Domain.Entities.Input>>(request.Inputs);
            var form = new Domain.Entities.Form
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Inputs = inputs,
                Timestamps = new Domain.ValueObjects.Timestamps()
            };
            foreach (var input in inputs)
            {
                input.Id = Guid.NewGuid();
                input.Form = form;
                await _dbContext.FormInputs.AddAsync(input, cancellationToken);
            }

            await _dbContext.Forms.AddAsync(form, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return form.Id;
        }
    }
}
