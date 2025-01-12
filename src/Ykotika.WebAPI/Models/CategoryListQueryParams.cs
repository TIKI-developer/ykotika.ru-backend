using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;
using Ykotika.WebAPI.Models.Binders;

namespace Ykotika.WebAPI.Models
{
    public class CategoryListQueryParams : IMapWith<GetCategoryListQuery>
    {
        [ModelBinder(BinderType = typeof(SortingBinder))]
        public SortingQueryDto Sorting { get; set; } = new();

        [ModelBinder(BinderType = typeof(PaginationBinder))]
        public PaginationQueryDto Pagination { get; set; } = new();

        [ModelBinder(BinderType = typeof(CategoryFilterBinder))]
        public CategoryFilterDto Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CategoryListQueryParams, GetCategoryListQuery>();
        }
    }
}
