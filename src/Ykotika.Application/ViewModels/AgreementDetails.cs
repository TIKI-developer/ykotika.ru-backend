using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.ViewModels
{
    public class AgreementDetails : IMapWith<Agreement>
    {
        public required Guid OfferId { get; set; }
        public required Author Author { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Agreement, AgreementDetails>()
                .ForMember(to => to.OfferId, 
                opt => opt.MapFrom(from => from.Offer.Id));
        }
    }
}
