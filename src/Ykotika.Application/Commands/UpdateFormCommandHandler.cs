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

        public async Task
            Handle(UpdateFormCommand request,
                   CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .Include(f => f.Inputs)
                .ThenInclude(e => e.ExtraAttributes)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Form), request.Id);

            form.Name = request.Name ?? form.Name;
            form.IsPublished = request.IsPublished ?? form.IsPublished;

            List<string> inputsToSaveIds = [];

            if (request.Inputs != null)
            {
                foreach (var (input, index) in request.Inputs.Select((input, index) => (input, index)))
                {
                    var formInput = form
                    .Inputs
                    .FirstOrDefault(e => e.Id == input.Id);

                    string id;

                    if (formInput == null)
                    {
                        var newInput = new Form.Input
                        {
                            Id = Nanoid.Generate(size: 6),
                            OrderIndex = index,
                            ExtraAttributes = input.ExtraAttributes,
                            DefaultValue = input.DefaultValue,
                            Type = input.Type,
                        };
                        form.Inputs.Add(newInput);
                        id = newInput.Id;
                    }
                    else
                    {
                        formInput.Type = input.Type;
                        formInput.OrderIndex = index;
                        formInput.DefaultValue = input.DefaultValue;
                        formInput.ExtraAttributes = input.ExtraAttributes;
                        id = formInput.Id;
                    }
                    inputsToSaveIds.Add(id);
                }
                form.Inputs.RemoveAll(input => !inputsToSaveIds.Contains(input.Id));
            }
            else
            {
                throw new Exception("Form must have at least one input!");
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
