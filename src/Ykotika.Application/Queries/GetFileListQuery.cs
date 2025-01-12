using MediatR;
using Ykotika.Application.Models;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetFileListQuery : IRequest<PagedList<FileItem>> 
    {
        public required PaginationDto Pagination { get; set; }
    }
}
