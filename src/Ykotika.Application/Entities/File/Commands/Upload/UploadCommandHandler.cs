using AutoMapper;
using MediatR;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.File.Commands.Upload
{
    public class UploadCommandHandler
        (IYkotikaDbContext dbContext,
        IFileService fileService,
        IMapper mapper)
        :
        IRequestHandler<UploadCommand, FileViewModel>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IFileService _fileService = fileService;

        public async Task<FileViewModel> Handle(UploadCommand request, CancellationToken cancellationToken)
        {
            FileModel file;
            if (request.RelativePath != null)
                file = await _fileService.Upload(request.FileData, request.RelativePath);
            else
                file = await _fileService.Upload(request.FileData);

            await _dbContext.Files.AddAsync(file, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FileViewModel>(file);
        }
    }
}
