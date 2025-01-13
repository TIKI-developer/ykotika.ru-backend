using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Queries;
using Ykotika.WebAPI.ModelBinders;

namespace Ykotika.WebAPI.Models
{
    public class FileListQueryParams : IMapWith<GetFileListQuery>
    {
        [ModelBinder(BinderType = typeof(PaginationBinder))]
        public PaginationQueryParams Pagination { get; set; } = new();
    }
}
