using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;

namespace Ykotika.WebAPI.Models
{
    public class FileListQueryParams : IMapWith<GetFileListQuery>
    {
        public required PaginationDto Pagination { get; set; } = new();
    }
}
