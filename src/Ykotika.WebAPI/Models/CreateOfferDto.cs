using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class CreateOfferDto : IMapWith<CreateOfferCommand>
    {
        public required string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOfferDto, CreateOfferCommand>();
        }
    }
}
