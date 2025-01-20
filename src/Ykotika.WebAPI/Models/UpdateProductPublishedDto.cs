using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateProductPublishedDto : IMapWith<UpdateProductPublishedCommand>
    {
        public required bool IsPublished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductPublishedDto, UpdateProductPublishedCommand>();
        }
    }
}
