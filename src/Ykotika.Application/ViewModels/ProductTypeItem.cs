using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class ProductTypeItem : IMapWith<ProductType>
    {
        public required Guid Id { get; set; }
        public required Guid FormId { get; set; }
        public required string Name { get; set; }
        public string? ManualLink { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductType, ProductTypeItem>()
                .ForMember(to => to.FormId,
                opt => opt.MapFrom(from => from.Form.Id));
        }
    }
}
