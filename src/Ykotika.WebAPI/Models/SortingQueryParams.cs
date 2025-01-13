using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.Models
{
    public class SortingQueryParams : IMapWith<SortingDto>
    {
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SortingQueryParams, SortingDto>();
        }
    }
}
