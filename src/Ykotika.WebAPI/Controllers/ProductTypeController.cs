using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.ModelBinders;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("products/types")]
    public class ProductTypeController
        (IMapper mapper,
        IAuthorizationService authorizationService)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        [HttpGet]
        public async Task<ActionResult<PagedList<ProductTypeItem>>>
            Get([FromQuery] ProductTypeListQueryParams queryParams)
        {
            var query = _mapper.Map<GetProductTypeListQuery>(queryParams);
            var authorizationResult = await
                _authorizationService
                .AuthorizeAsync
                (User, new PublishableResourceDto { IsPublished = query.Filter.IsPublished },
                Policies.PRODUCT_TYPE_LIST_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}, {Roles.DIRECTOR_ROLE}, {Roles.ADMIN_ROLE}")]
        public async Task<ActionResult<ProductTypeDetails>>
            GetById(Guid id)
        {
            var query = new GetProductTypeByIdQuery { Id = id };
            var vm = await Mediator.Send(query);

            var authorizationResult = await
                _authorizationService
                .AuthorizeAsync(User, vm, Policies.CONTENT_POLICY);

            if (authorizationResult.Succeeded)
            {
                return Ok(vm);
            }

            return Forbid();
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPost]
        public async Task<ActionResult<Guid>>
            Create([FromBody] CreateProductTypeDto dto)
        {
            var command = _mapper.Map<CreateProductTypeCommand>(dto);
            command.AuthorId = UserId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPut("{id}")]
        public async Task<IActionResult>
            Update(Guid id, [FromBody] UpdateProductTypeDto dto)
        {
            var command = _mapper.Map<UpdateProductTypeCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult>
            Delete(Guid id)
        {
            var command = new DeleteProductTypeCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
    }
    public class ProductTypeListQueryParams : IMapWith<GetProductTypeListQuery>
    {
        [ModelBinder(BinderType = typeof(SortingBinder))]
        public SortingQueryParams Sorting { get; set; } = new();

        [ModelBinder(BinderType = typeof(PaginationBinder))]
        public PaginationQueryParams Pagination { get; set; } = new();
        public ProductTypeFilterQueryParams Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductTypeListQueryParams, GetProductTypeListQuery>();
        }
    }
    public class ProductTypeFilterQueryParams : IMapWith<ProductTypeFilterDto>
    {
        public string? IsPublished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductTypeFilterQueryParams, ProductTypeFilterDto>()
                .ForMember(to => to.IsPublished,
                opt => opt.MapFrom(from => from.IsPublished));
        }
    }
}