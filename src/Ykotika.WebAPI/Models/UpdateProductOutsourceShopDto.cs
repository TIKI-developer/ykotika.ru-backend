using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UpdateProductOutsourceShopDto : IMapWith<UpdateProductOutsourceShopCommand>
    {
        public required List<Guid> OutsourceShops { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductOutsourceShopDto, UpdateProductOutsourceShopCommand>();
        }
    }
}
