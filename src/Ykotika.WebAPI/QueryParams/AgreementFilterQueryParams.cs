using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.QueryParams
{
    public class AgreementFilterQueryParams : IMapWith<AgreementFilterDto>
    {
        public string? UserId { get; set; }
        public string? OfferId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AgreementFilterQueryParams, AgreementFilterDto>()
                .ForMember(to => to.OfferId,
                opt => opt.MapFrom(from => from.OfferId))
                .ForMember(to => to.UserId,
                opt => opt.MapFrom(from => from.UserId));
        }
    }
}
