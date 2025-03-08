using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.ModelBinders;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("products")]
    public class ProductController
        (IMapper mapper,
        IAuthorizationService authorizationService)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        [HttpGet]
        public async Task<ActionResult<PagedList<ProductItem>>>
            Get([FromQuery] ProductListQueryParams queryParams)
        {
            var query = _mapper.Map<GetProductListQuery>(queryParams);
            var authorizationResult = await
                _authorizationService
                .AuthorizeAsync
                (User, new PublishableResourceDto { IsPublished = query.Filter.IsPublished },
                Policies.PRODUCT_LIST_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("me")]
        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}")]
        public async Task<ActionResult<PagedList<ProductItem>>>
            GetMy([FromQuery] ProductListQueryParams queryParams)
        {
            var query = _mapper.Map<GetProductListQuery>(queryParams);
            query.Filter.UserId = UserId;
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetails>>
            GetById(Guid id)
        {
            var query = new GetProductByIdQuery() { Id = id };
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

        [HttpPost]
        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}")]
        public async Task<ActionResult<Guid>>
            Create([FromBody] CreateProductDto dto)
        {
            var command = _mapper.Map<CreateProductCommand>(dto);
            command.UserId = UserId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{Roles.MODERATOR_ROLE}, {Roles.AUTHOR_ROLE}")]
        public async Task<IActionResult>
            Update(Guid id, [FromBody] UpdateProductDto dto)
        {
            var command = _mapper.Map<UpdateProductCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Roles.MODERATOR_ROLE}, {Roles.AUTHOR_ROLE}")]
        public async Task<IActionResult>
            Delete(Guid id)
        {
            var command = new DeleteProductCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("generate-spreadsheet")]
        [Authorize(Roles = $"{Roles.MODERATOR_ROLE}")]
        public async Task<ActionResult<Guid>>
            GenerateSpreadSheet([FromBody] GenerateProductSpreadsheetDto dto)
        {
            var command = _mapper.Map<GenerateProductSpreadsheetCommand>(dto);
            command.RootUrl = Request.Headers.Origin.ToString() ?? $"{Request.Scheme}://{Request.Host}";
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPost("generate-catalog")]
        [Authorize(Roles = $"{Roles.MODERATOR_ROLE}")]
        public async Task<IActionResult>
            GenerateCatalog([FromBody] GenerateProductSourcesDto dto)
        {
            var command = _mapper.Map<GenerateProductSourcesCommand>(dto);
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("{id}/outsource-shops")]
        [Authorize(Roles = $"{Roles.MODERATOR_ROLE}, {Roles.ADMIN_ROLE}, {Roles.DIRECTOR_ROLE}")]
        public async Task<IActionResult>
            ChangeOutsourceShops(Guid id, [FromBody] UpdateProductOutsourceShopDto dto)
        {
            var command = _mapper.Map<UpdateProductOutsourceShopCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("{id}/comments")]
        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}, {Roles.MODERATOR_ROLE}")]
        public async Task<IActionResult>
            CreateComment(Guid id, [FromBody] CreateProductCommentDto dto)
        {
            var vm = new GetProductByIdQuery
            {
                Id = id
            };

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, vm, Policies.POST_PRODUCT_COMMENT_POLICY);

            if (authorizationResult.Succeeded)
            {
                var command = _mapper.Map<CreateProductCommentCommand>(dto);
                command.Id = id;
                command.UserId = UserId;
                await Mediator.Send(command);

                return Ok();
            }

            return Forbid();
        }
        [HttpPost("duplicate/{id}")]
        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}, {Roles.MODERATOR_ROLE}, {Roles.ADMIN_ROLE}")]
        public async Task<IActionResult>
            Duplicate(Guid id)
        {
            var vm = new GetProductByIdQuery
            {
                Id = id
            };

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, vm, Policies.PRODUCT_DUPLICATE_POLICY);

            if (authorizationResult.Succeeded)
            {
                var command = new DuplicateProductCommand
                {
                    Id = id,
                    UserId = UserId
                };

                await Mediator.Send(command);

                return Ok();
            }

            return Forbid();
        }
        [HttpPatch("{id}/published")]
        [Authorize(Roles = $"{Roles.ADMIN_ROLE}, {Roles.MODERATOR_ROLE}")]
        public async Task<IActionResult>
            UpdatePublished(Guid id, [FromBody] UpdateProductPublishedDto dto)
        {
            var command = _mapper.Map<UpdateProductPublishedCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [HttpPatch("{id}/status")]
        [Authorize(Roles = $"{Roles.ADMIN_ROLE}, {Roles.MODERATOR_ROLE}, {Roles.AUTHOR_ROLE}")]
        public async Task<IActionResult>
            UpdateStatus(Guid id, [FromBody] UpdateProductStatusDto dto)
        {
            var command = _mapper.Map<UpdateProductStatusCommand>(dto);
            command.Id = id;

            var query = new GetProductByIdQuery { Id = id };
            var vm = await Mediator.Send(query);

            var authorizationDto = new UpdateProductStatusAuthorizationDto
            {
                From = Enum.Parse<ProductStatus>(vm.Status),
                To = Enum.Parse<ProductStatus>(dto.NewStatus)
            };

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, authorizationDto, Policies.PRODUCT_STATUS_POLICY);

            if (authorizationResult.Succeeded)
            {
                await Mediator.Send(command);

                return Ok();
            }
            else
            {
                return Forbid();
            }
        }
        [HttpPatch("status")]
        [Authorize(Roles = $"{Roles.ADMIN_ROLE}, {Roles.MODERATOR_ROLE}")]
        public async Task<IActionResult>
            UpdateStatusMultiple([FromBody] UpdateProductStatusMultipleDto dto)
        {
            foreach (var id in dto.ProductIds)
            {
                var command = new UpdateProductStatusCommand
                {
                    Id = id,
                    NewStatus = Enum.Parse<ProductStatus>(dto.NewStatus)
                };
                var query = new GetProductByIdQuery { Id = id };
                var vm = await Mediator.Send(query);

                var authorizationDto = new UpdateProductStatusAuthorizationDto
                {
                    From = Enum.Parse<ProductStatus>(vm.Status),
                    To = Enum.Parse<ProductStatus>(dto.NewStatus)
                };

                var authorizationResult = await _authorizationService.AuthorizeAsync(User, authorizationDto, Policies.PRODUCT_STATUS_POLICY);

                if (authorizationResult.Succeeded)
                {
                    await Mediator.Send(command);
                }
            }

            return Ok();
        }
        [HttpPost("by-spreadsheet")]
        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}, {Roles.ADMIN_ROLE}, {Roles.MODERATOR_ROLE}")]
        public async Task<IActionResult>
            UploadProductsBySpreadsheet([FromBody] UploadProductsBySpreadsheetDto dto)
        {
            var command = _mapper.Map<CreateProductsBySpreadsheetCommand>(dto);
            await Mediator.Send(command);

            return Ok();
        }
    }
    public class ProductListQueryParams : IMapWith<GetProductListQuery>
    {
        [ModelBinder(BinderType = typeof(SortingBinder))]
        public SortingQueryParams Sorting { get; set; } = new();

        [ModelBinder(BinderType = typeof(PaginationBinder))]
        public PaginationQueryParams Pagination { get; set; } = new();

        [ModelBinder(BinderType = typeof(ProductFilterBinder))]
        public ProductFilterQueryParams Filter { get; set; } = new();

        [FromQuery(Name = "searchTerm")]
        public string? SearchTerm { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductListQueryParams, GetProductListQuery>();
        }
    }
    public class ProductFilterQueryParams : IMapWith<ProductFilterDto>
    {
        public string? IsPublished { get; set; }
        public string? Status { get; set; }
        public string? UserId { get; set; }
        public string? ProductTypeId { get; set; }
        public string? CategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductFilterQueryParams, ProductFilterDto>()
                .ForMember(to => to.Status,
                    opt => opt.MapFrom(from => from.Status != null ? Enum.Parse<ProductStatus>(from.Status) : (ProductStatus?)null))
                .ForMember(to => to.IsPublished,
                    opt => opt.MapFrom(from =>
                        string.IsNullOrEmpty(from.IsPublished) ? (bool?)null :
                        (from.IsPublished.Equals("true", StringComparison.OrdinalIgnoreCase) ? (bool?)true : (bool?)false)))
                .ForMember(to => to.UserId,
                    opt => opt.MapFrom(from =>
                        string.IsNullOrEmpty(from.UserId) ? (Guid?)null : Guid.Parse(from.UserId)))
                .ForMember(to => to.ProductTypeId,
                    opt => opt.MapFrom(from =>
                        string.IsNullOrEmpty(from.ProductTypeId) ? (Guid?)null : Guid.Parse(from.ProductTypeId)));
        }
    }
}
