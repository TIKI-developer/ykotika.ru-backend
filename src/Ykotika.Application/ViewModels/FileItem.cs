using AutoMapper;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.Application.ViewModels
{
    public class FileItem : IMapWith<Domain.Entities.File>
    {
        public required string Path { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.File, FileItem>();
        }
    }
}
