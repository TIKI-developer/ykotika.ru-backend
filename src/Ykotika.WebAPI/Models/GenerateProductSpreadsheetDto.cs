using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class GenerateProductSpreadsheetDto : IMapWith<GenerateProductSpreadsheetCommand>
    {
        public required List<Guid> Products { get; set; }
    
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GenerateProductSpreadsheetDto, GenerateProductSpreadsheetCommand>();
        }
    }
}
