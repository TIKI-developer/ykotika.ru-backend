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
    [Route("forms")]
    [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
    public class FormController(IMapper mapper) : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateFormDto dto)
        {
            var command = _mapper.Map<CreateFormCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFormDto dto)
        {
            var command = _mapper.Map<UpdateFormCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteFormCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FormDetails>> Get(Guid id)
        {
            var query = new GetFormQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpGet]
        public async Task<ActionResult<FormList>>
            Get([FromQuery] FormFilterDto filter)
        {
            var query = new GetFormListQuery
            {
                IsPublished = filter.IsPublished
            };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpGet("published")]
        public async Task<ActionResult<FormList>> GetPublished()
        {
            var query = new GetFormListQuery
            {
                IsPublished = true
            };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPost("{formId}/inputs")]
        public async Task<ActionResult<Guid>> AddInput(Guid formId, [FromBody] AddInputDto dto)
        {
            var command = _mapper.Map<AddInputCommand>(dto);
            command.FormId = formId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPut("inputs/{id}")]
        public async Task<IActionResult> UpdateInput(Guid id, [FromBody] UpdateInputDto dto)
        {
            var command = _mapper.Map<UpdateInputCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpDelete("inputs/{id}")]
        public async Task<IActionResult> RemoveInput(Guid id)
        {
            var command = new DeleteInputCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
    }
}
