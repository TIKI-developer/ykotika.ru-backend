using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class CreateMessageDto : IMapWith<CreateMessageCommand>
    {
        public string? Text { get; set; }
        public List<string>? Attachments { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMessageDto, CreateMessageCommand>();
        }
    }
}
