using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;

namespace Ykotika.WebAPI.Models
{
    public class AgreementListQueryParams : IMapWith<GetAgreementListQuery>
    {
        public required PaginationDto Pagination { get; set; } = new();
        public required SortingDto Sorting { get; set; } = new();
        public required AgreementFilterDto Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AgreementListQueryParams, GetAgreementListQuery>();
        }
    }
}
