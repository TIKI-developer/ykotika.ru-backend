using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Entities.Form.Commands.AddInput;
using Ykotika.Application.Entities.Form.Commands.Create;
using Ykotika.Application.Entities.Form.Commands.Delete;
using Ykotika.Application.Entities.Form.Commands.DeleteInput;
using Ykotika.Application.Entities.Form.Commands.Update;
using Ykotika.Application.Entities.Form.Commands.UpdateInput;
using Ykotika.Application.Entities.Form.Queries.GetById;
using Ykotika.Application.Entities.Form.Queries.GetList;
using Ykotika.WebAPI.Models.Forms;

namespace Ykotika.WebAPI.Controllers
{
    [Route("forms")]
    public class FormController(IMapper mapper) : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateFormDto dto)
        {
            var command = _mapper.Map<CreateFormCommand>(dto);

            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFormDto dto)
        {
            var command = _mapper.Map<UpdateFormCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteFormCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FormViewModel>> Get(Guid id)
        {
            var query = new GetFormQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpGet]
        public async Task<ActionResult<FormListViewModel>> Get()
        {
            var query = new GetFormsQuery();
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost("add-input/{formId}")]
        public async Task<ActionResult<Guid>> AddInput(Guid formId, [FromBody] AddInputDto dto)
        {
            var command = _mapper.Map<AddInputCommand>(dto);
            command.FormId = formId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [HttpPut("update-input/{id}")]
        public async Task<IActionResult> UpdateInput(Guid id, [FromBody] UpdateInputDto dto)
        {
            var command = _mapper.Map<UpdateInputCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [HttpDelete("delete-input/{id}")]
        public async Task<IActionResult> RemoveInput(Guid id)
        {
            var command = new DeleteInputCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }

    }
}
