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
    [Route("categories")]
    public class CategoryController
        (IMapper mapper,
        IAuthorizationService authorizationService)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        [HttpGet]
        public async Task<ActionResult<CategoryList>>
            Get([FromQuery]
                bool? isPublished,
                string? sortBy,
                bool? desc)
        {
            var authorizationResult = await 
                _authorizationService
                .AuthorizeAsync(User, new ContentResourceDto { IsPublished = isPublished }, Policies.CATEGORY_LIST_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var query = new GetCategoryListQuery
            {
                IsPublished = isPublished,
                SortBy = sortBy,
                IsDescending = desc ?? false
            };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDetails>>
            GetById(Guid id)
        {
            var query = new GetCategoryByIdQuery { Id = id };
            var vm = await Mediator.Send(query);
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, vm, Policies.CONTENT_POLICY);
            
            if (authorizationResult.Succeeded)
            {
                return Ok(vm);
            }
            return Forbid();
        }
        [HttpPost]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<ActionResult<Guid>>
            Create([FromBody] CreateCategoryDto dto)
        {
            var command = _mapper.Map<CreateCategoryCommand>(dto);
            command.AuthorId = UserId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<IActionResult>
            Update(Guid id, [FromBody] UpdateCategoryDto dto)
        {
            var command = _mapper.Map<UpdateCategoryCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<IActionResult>
            Delete(Guid id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
    }
}
