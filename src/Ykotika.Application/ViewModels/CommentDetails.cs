using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class CommentDetails : IMapWith<Comment>
    {
        public required string SenderName { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentDetails>()
                .ForMember(to => to.SenderName,
                opt => opt.MapFrom(from => $"{from.Author.Name}"));
        }
    }
}
