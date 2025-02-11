using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Models
{
    public class UpdateProductStatusDto : IMapWith<UpdateProductStatusCommand>
    {
        public required string NewStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductStatusDto, UpdateProductStatusCommand>()
                .ForMember(to => to.NewStatus,
                opt => opt.MapFrom(from => Enum.Parse(typeof(ProductStatus), from.NewStatus)));
        }
    }
}
