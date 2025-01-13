using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.QueryParams
{
    public class PaginationQueryParams : IMapWith<PaginationDto>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PaginationQueryParams, PaginationDto>();
        }
    }
}
