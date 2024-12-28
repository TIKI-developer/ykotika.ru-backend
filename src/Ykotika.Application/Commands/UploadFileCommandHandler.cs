using AutoMapper;
using MediatR;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class UploadFileCommandHandler
        (IYkotikaDbContext dbContext,
        IFileService fileService,
        IMapper mapper)
        :
        IRequestHandler<UploadFileCommand, FileDetails>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IFileService _fileService = fileService;

        public async Task<FileDetails> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.File file;
            if (request.RelativePath != null)
                file = await _fileService.Upload(request.FileData, request.RelativePath);
            else
                file = await _fileService.Upload(request.FileData);

            await _dbContext.Files.AddAsync(file, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FileDetails>(file);
        }
    }
}
