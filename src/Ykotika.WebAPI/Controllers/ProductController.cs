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
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [Authorize(Roles = $"{Roles.MODERATOR_ROLE}")]
        [HttpGet]
        public async Task<ActionResult<ProductList>>
            Get([FromQuery] ProductFilterDto filter)
        {
            var query = new GetProductListQuery
            {
                IsPublished = filter.IsPublished,
                UserId = filter.UserId,
                ProductType = filter.ProductType
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}")]
        [HttpGet("my")]
        public async Task<ActionResult<ProductList>>
            GetMy([FromQuery] Guid? productType,
                  [FromQuery] bool? isPublished)
        {
            var query = new GetProductListQuery
            {
                IsPublished = isPublished,
                UserId = UserId,
                ProductType = productType
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("published")]
        public async Task<ActionResult<ProductList>>
            GetPublished([FromQuery] Guid? userId,
                         [FromQuery] Guid? productType)
        {
            var query = new GetProductListQuery
            {
                UserId = userId,
                ProductType = productType,
                IsPublished = true
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetails>>
            GetById(Guid id)
        {
            var query = new GetProductByIdQuery() { Id = id };
            var vm = await Mediator.Send(query);

            if (vm.IsPublished == false &&
                !User.IsInRole(Roles.MODERATOR_ROLE) &&
                UserId != vm.FormRecord.Author.Id)
            {
                return Forbid();
            }

            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>>
            Create([FromBody] CreateProductDto dto)
        {
            var command = _mapper.Map<CreateProductCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>
            Update(Guid id, [FromBody] UpdateProductDto dto)
        {
            var command = _mapper.Map<UpdateProductCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            Delete(Guid id)
        {
            var command = new DeleteProductCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("generate-spreadsheet")]
        public async Task<ActionResult<Guid>>
            GenerateSpreadSheet([FromBody] GenerateProductSpreadsheetDto dto)
        {
            var command = _mapper.Map<GenerateProductSpreadsheetCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPost("generate-catalog")]
        public async Task<IActionResult>
            GenerateCatalog([FromBody] GenerateProductSourcesDto dto)
        {
            var command = _mapper.Map<GenerateProductSourcesCommand>(dto);
            await Mediator.Send(command);

            return Ok();
        }

        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPatch("outsource-shops")]
        public async Task<IActionResult>
            ChangeOutsourceShops([FromBody] ChangeProductOutsourceShopDto dto)
        {
            var command = _mapper.Map<ChangeProductOutsourceShopCommand>(dto);
            await Mediator.Send(command);

            return Ok();
        }
    }
}
