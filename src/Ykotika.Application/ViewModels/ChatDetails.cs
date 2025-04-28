using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class ChatDetails : IMapWith<Chat>
    {
        public required Guid Id { get; set; }
        public required Timestamps Timestamps { get; set; }
        public string? Name { get; set; }
        public required List<Guid> Members { get; set; }
        public List<MessageItem> Messages { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Chat, ChatDetails>()
                .ForMember(to => to.Members, opt => opt.MapFrom(from => from.Members.Select(e => e.Id)));
        }
    }
}
