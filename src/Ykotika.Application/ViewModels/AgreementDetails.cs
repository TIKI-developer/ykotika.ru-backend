using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class AgreementDetails : IMapWith<Agreement>
    {
        public required Guid Id { get; set; }
        public required Guid OfferId { get; set; }
        public required Guid AuthorId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Agreement, AgreementDetails>()
                .ForMember(to => to.OfferId,
                opt => opt.MapFrom(from => from.Offer.Id))
                .ForMember(to => to.AuthorId,
                opt => opt.MapFrom(from => from.Author.UserId));
        }
    }
}
