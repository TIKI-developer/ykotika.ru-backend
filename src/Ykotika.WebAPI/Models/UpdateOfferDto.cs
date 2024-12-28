using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateOfferDto : IMapWith<UpdateOfferCommand>
    {
        public string? Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOfferDto, UpdateOfferCommand>();
        }
    }
}
