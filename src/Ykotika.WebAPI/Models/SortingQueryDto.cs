using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.Models
{
    public class SortingQueryDto : IMapWith<SortingDto>
    {
        [FromQuery(Name = "sortBy")]
        public string? SortBy { get; set; }
        [FromQuery(Name = "desc")]
        public bool IsDescending { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SortingQueryDto, SortingDto>();
        }
    }
}
