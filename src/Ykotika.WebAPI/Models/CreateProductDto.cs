using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class CreateProductDto : IMapWith<CreateProductCommand>
    {
        public required Guid ProductTypeId { get; set; }
        public required Guid FormRecordId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, CreateProductCommand>();
        }
    }
}
