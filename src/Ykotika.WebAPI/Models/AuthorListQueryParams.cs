using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;

namespace Ykotika.WebAPI.Models
{
    public class AuthorListQueryParams : IMapWith<GetAuthorListQuery>
    {
        public required PaginationDto Pagination { get; set; } = new();
        public required SortingDto Sorting { get; set; } = new();
        public required AuthorFilterDto Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthorListQueryParams, GetAuthorListQuery>();
        }
    }
}
