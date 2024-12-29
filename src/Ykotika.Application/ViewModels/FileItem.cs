using AutoMapper;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.Application.ViewModels
{
    public class FileItem : IMapWith<Domain.Entities.File>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string RelativePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.File, FileItem>();
        }
    }
}
