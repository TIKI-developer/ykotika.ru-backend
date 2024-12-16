using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Queries.GetList
{
    public class FormLookupDto : IMapWith<FormModel>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormModel, FormLookupDto>();
        }
    }
}
