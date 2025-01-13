using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.Models;
using Ykotika.WebAPI.QueryParams;

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
            var authorizationResult = await
                _authorizationService
                .AuthorizeAsync
                (User, new PublishableResourceDto { IsPublished = queryParams.Filter.IsPublished },
                Policies.PRODUCT_TYPE_LIST_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var query = _mapper.Map<GetProductTypeListQuery>(queryParams);
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
}