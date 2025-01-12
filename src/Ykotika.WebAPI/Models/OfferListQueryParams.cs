using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;

namespace Ykotika.WebAPI.Models
{
    public class OfferListQueryParams : IMapWith<GetOfferListQuery>
    {
        public required SortingDto Sorting { get; set; } = new();
        public required OfferFilterDto Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OfferListQueryParams, GetOfferListQuery>();
        }
    }
}
