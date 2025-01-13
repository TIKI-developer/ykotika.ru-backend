using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.Models
{
    public class CategoryFilterQueryParams : IMapWith<CategoryFilterDto>
    {
        public string? IsPublished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CategoryFilterQueryParams, CategoryFilterDto>()
                .ForMember(to => to.IsPublished,
                opt => opt.MapFrom(from => from.IsPublished));
        }
    }
}
