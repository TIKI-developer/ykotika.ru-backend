using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;

namespace Ykotika.WebAPI.Models
{
    public class UserListQueryParams : IMapWith<GetUserListQuery>
    {
        public required PaginationDto Pagination { get; set; } = new();
        public required SortingDto Sorting { get; set; } = new();
        public required UserFilterDto Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserListQueryParams, GetUserListQuery>();
        }
    }
}
