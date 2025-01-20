using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class CreateProductCommentDto : IMapWith<CreateProductCommentCommand>
    {
        public required string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductCommentDto, CreateProductCommentCommand>();
        }
    }
}
