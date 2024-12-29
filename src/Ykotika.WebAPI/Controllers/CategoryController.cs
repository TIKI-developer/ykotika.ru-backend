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
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpGet]
        public async Task<ActionResult<CategoryList>> Get()
        {
            var query = new GetCategoryListQuery();
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDetails>> GetById(Guid id)
        {
            var query = new GetCategoryByIdQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCategoryDto dto)
        {
            var command = _mapper.Map<CreateCategoryCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto dto)
        {
            var command = _mapper.Map<UpdateCategoryCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
    }
}
