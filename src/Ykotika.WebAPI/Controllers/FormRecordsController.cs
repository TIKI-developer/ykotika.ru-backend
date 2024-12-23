using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Entities.FormRecord.Commands.Create;
using Ykotika.Application.Entities.FormRecord.Commands.Delete;
using Ykotika.Application.Entities.FormRecord.Commands.Update;
using Ykotika.Application.Entities.FormRecord.Queries.GetById;
using Ykotika.Application.Entities.FormRecord.Queries.GetList;
using Ykotika.WebAPI.Models.Forms;

namespace Ykotika.WebAPI.Controllers
{
    [Route("forms/records")]
    public class FormRecordsController(IMapper mapper) : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateFormRecordDto dto)
        {
            var command = _mapper.Map<CreateFormRecordCommand>(dto);
            command.UserId = UserId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFormRecordDto dto)
        {
            var command = _mapper.Map<UpdateFormRecordCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteFormRecordCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FormRecordViewModel>> Get(Guid id)
        {
            var query = new GetFormRecordQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpGet]
        public async Task<ActionResult<FormRecordListViewModel>> Get()
        {
            var query = new GetFormRecordListQuery();
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
    }
}
