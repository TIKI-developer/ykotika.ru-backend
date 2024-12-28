using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class OfferItem : IMapWith<Offer>
    {
        public required Guid Id { get; set; }
        public required Timestamps Timestamps { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Offer, OfferItem>()
                .ForMember(to => to.Id,
                opt => opt.MapFrom(from => from.Id));
        }
    }
}
