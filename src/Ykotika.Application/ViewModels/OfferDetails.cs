using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class OfferDetails : IMapWith<Offer>, IPublishable
    {
        public required Guid Id { get; set; }
        public required string Content { get; set; }
        public required bool IsPublished { get; set; }
        public required Timestamps Timestamps { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Offer, OfferDetails>();
        }
    }
}
