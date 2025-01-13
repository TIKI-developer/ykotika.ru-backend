using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;
using Ykotika.WebAPI.ModelBinders;

namespace Ykotika.WebAPI.Models
{
    public class ProductListQueryParams : IMapWith<GetProductListQuery>
    {
        [ModelBinder(BinderType = typeof(SortingBinder))]
        public SortingQueryParams Sorting { get; set; } = new();

        [ModelBinder(BinderType = typeof(PaginationBinder))]
        public PaginationQueryParams Pagination { get; set; } = new();

        [ModelBinder(BinderType = typeof(ProductFilterBinder))]
        public required ProductFilterDto Filter { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductListQueryParams, GetProductListQuery>();
        }
    }
}
