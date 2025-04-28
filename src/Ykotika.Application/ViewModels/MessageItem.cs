using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class MessageItem : IMapWith<Message>
    {
        public required Guid Id { get; set; }
        public required Timestamps Timestamps { get; set; }
        public string? Text { get; set; }
        public List<string>? Attachments { get; set; }
        public required Guid SenderId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Message, MessageItem>()
                .ForMember(to => to.Attachments, 
                opt => opt.MapFrom(from => from.Attachments.Select(e => e.Path)))
                .ForMember(to => to.SenderId,
                opt => opt.MapFrom(from => from.Sender.Id));
        }
    }
}
