using AutoMapper;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class UploadProductsBySpreadsheetDto : IMapWith<CreateProductsBySpreadsheetCommand>
    {
        public required string SpreadsheetFilePath { get; set; }
        public required string ZipFilePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UploadProductsBySpreadsheetDto, CreateProductsBySpreadsheetCommand>();
        }
    }
}
