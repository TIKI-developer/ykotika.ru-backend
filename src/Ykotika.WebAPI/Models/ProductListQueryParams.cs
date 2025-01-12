using AutoMapper;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;

namespace Ykotika.WebAPI.Models
{
    public class ProductListQueryParams : IMapWith<GetProductListQuery>
    {
        public required PaginationDto Pagination { get; set; }
        public required SortingDto Sorting { get; set; }
        public required ProductFilterDto Filter { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductListQueryParams, GetProductListQuery>();
        }
    }
}
