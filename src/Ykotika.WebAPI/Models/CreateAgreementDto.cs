using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class CreateAgreementDto : IMapWith<CreateAgreementCommand>
    {
        public Guid OfferId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAgreementDto, CreateAgreementCommand>();
        }
    }
}
