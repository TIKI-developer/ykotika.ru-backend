using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class AgreementItem : IMapWith<Agreement>
    {
        public required Guid Id { get; set; }
        public required Guid OfferId { get; set; }
        public required DateTime AcceptedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Agreement, AgreementItem>()
                .ForMember(to => to.OfferId,
                opt => opt.MapFrom(from => from.Offer.Id))

                .ForMember(to => to.AcceptedAt,
                opt => opt.MapFrom(from => from.Timestamps.CreatedAt));
        }
    }
}
