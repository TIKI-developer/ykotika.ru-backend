using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;
using Ykotika.WebAPI.ModelBinders;

namespace Ykotika.WebAPI.Models
{
    public class OfferListQueryParams : IMapWith<GetOfferListQuery>
    {
        [ModelBinder(BinderType = typeof(SortingBinder))]
        public SortingQueryParams Sorting { get; set; } = new();

        public required OfferFilterDto Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OfferListQueryParams, GetOfferListQuery>();
        }
    }
}
