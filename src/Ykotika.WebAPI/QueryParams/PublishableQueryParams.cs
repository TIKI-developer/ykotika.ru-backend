using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.QueryParams
{
    public class PublishableQueryParams : IMapWith<PublishableFilterDto>
    {
        public string? IsPublished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PublishableQueryParams, PublishableFilterDto>()
                .ForMember(to => to.IsPublished,
                opt => opt.MapFrom(from => bool.Parse(from.IsPublished)));
        }
    }
}
