using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateProductDto : IMapWith<UpdateProductCommand>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<Guid>? OutsourceShops { get; set; }
        public List<Guid>? Images { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductDto, UpdateProductCommand>();
        }
    }
}
