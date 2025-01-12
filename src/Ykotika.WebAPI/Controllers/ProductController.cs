using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Constants;
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
            var authorizationResult = await
                _authorizationService
                .AuthorizeAsync
                (User, new ContentResourceDto { IsPublished = queryParams.Filter.IsPublished },
                Policies.PRODUCT_LIST_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var query = _mapper.Map<GetProductListQuery>(queryParams);

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

        [HttpPatch("outsource-shops")]
        [Authorize(Roles = $"{Roles.MODERATOR_ROLE}, {Roles.ADMIN_ROLE}, {Roles.DIRECTOR_ROLE}")]
        public async Task<IActionResult>
            ChangeOutsourceShops([FromBody] UpdateProductOutsourceShopDto dto)
        {
            var command = _mapper.Map<UpdateProductOutsourceShopCommand>(dto);
            await Mediator.Send(command);

            return Ok();
        }
    }
}
