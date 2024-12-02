using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Entities.Form.Commands.Create;
using Ykotika.Application.Entities.Form.Commands.Delete;
using Ykotika.Application.Entities.Form.Commands.Update;
using Ykotika.Application.Entities.Form.Queries.GetById;
using Ykotika.Application.Entities.Form.Queries.GetList;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("submitted-forms")]
    public class SubmittedFormController(IMapper mapper) : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromForm] CreateFormDto dto)
        {
            var command = _mapper.Map<CreateFormCommand>(dto);

            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateFormDto dto)
        {
            var command = _mapper.Map<UpdateFormCommand>(dto);
            command.Id = UserId;
            await Mediator.Send(command);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteFormCommand { Id =  id };
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
    }
}
