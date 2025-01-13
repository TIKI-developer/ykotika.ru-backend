using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.ViewModels
{
    public class FileDetails : IMapWith<Domain.Entities.File>
    {
        public required string Path { get; set; }
        public required Timestamps Timestamps { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.File, FileDetails>();
        }
    }
}
